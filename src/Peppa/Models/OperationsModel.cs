﻿using piggy_bank_uwp.Interface.Models;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Interface;
using Peppa.Interface.Services;
using System.Collections.Generic;
using Peppa.Context.Entities;
using Peppa.Dto;
using System.Linq;

namespace Peppa.Models
{
    public class OperationsModel : IOperationsModel
    {
        private readonly IPiggyRepository _repository;
        private readonly IOperationService _service;
        private readonly Dictionary<string, string> _availableCurrencies;

        public OperationsModel(IPiggyRepository repository, IOperationService service)
        {
            _repository = repository;
            _service = service;
            _availableCurrencies = new Dictionary<string, string>
            {
                {"RUB", "₽"},
                {"BYN", "Br"},
                {"UAH", "₴"},
                {"KZT", "₸"},
                {"USD", "$"},
                {"EUR", "€"}
            };
        }

        public IPiggyRepository Repository => _repository;

        public async Task<PageResult<Operation>> GetOperations(int page, CancellationToken token)
        {
            //If user is logged in then sync accounts
            if (_service.IsAuthorized)
            {
                var response = await _service.GetOperations(page, token);
                if (response != null)
                {

                    var entities = response.Result.Select(o => new Operation
                    {
                        Id = o.Id,
                        IsDeleted = o.IsDeleted,
                        AccountTitle = o.Account.Title,
                        ToTitle = o.ToAccount?.Title,
                        CategoryType = o.Category?.Type,
                        CategoryHexColor = o.Category?.HexColor,
                        CategoryTitle = o.Category?.Title,
                        Amount = o.Amount,
                        Type = o.Type,
                        CreatedOn = o.Date
                    });

                    //TODO Save into db

                    return new PageResult<Operation>
                    {
                        TotalPages = response.TotalPages,
                        CurrentPage = response.CurrentPage,
                        Result = response.Result.Select(o => new Operation
                        {
                            Id = o.Id,
                            IsDeleted = o.IsDeleted,
                            AccountTitle = o.Account.Title,
                            ToTitle = o.ToAccount?.Title,
                            CategoryType = o.Category?.Type,
                            CategoryHexColor = o.Category?.HexColor,
                            CategoryTitle = o.Category?.Title,
                            Amount = o.Amount,
                            Type = o.Type,
                            CreatedOn = o.Date
                        }).ToArray()
                    };
                }
            }

            return null;
            //return await _repository.GetOperations(token);
        }

        public Task<Account[]> GetAccounts(CancellationToken token)
           => _repository.GetAccounts(token, all: false);

        public Task<Category[]> GetCategories(CancellationToken token)
            => _repository.GetCategories(token, all: false);

        public void Dispose()
        {
            _repository?.Dispose();
        }

        public async Task<Operation> GetBudgetOperation(int operationId, CancellationToken token)
        {
            if (_service.IsAuthorized)
            {
                var response = await _service.GetBudgetOperation(operationId, token);
                if (response != null)
                {
                    var entity = new Operation
                    {
                        Id = response.Id,
                        CategoryId = response.CategoryId,
                        AccountId = response.AccountId,
                        Amount = response.Amount,
                        CreatedOn = response.Date,
                        Type = response.Type,
                        Comment = response.Comment
                    };

                    await _repository.AddOrUpdateOperation(entity, token);
                }
            }

            return await _repository.GetOperation(operationId, token);
        }

        public async Task<Operation> GetTransaferOperation(int operationId, CancellationToken token)
        {
            if (_service.IsAuthorized)
            {
                var response = await _service.GetTransferOperation(operationId, token);
                if (response != null)
                {
                    var entity = new Operation
                    {
                        Id = response.Id,
                        AccountId = response.FromId,
                        ToId = response.ToId,
                        Amount = response.Amount,
                        CreatedOn = response.Date,
                        Type = response.Type,
                        Comment = response.Comment
                    };

                    await _repository.AddOrUpdateOperation(entity, token);
                }
            }

            return await _repository.GetOperation(operationId, token);
        }
    }
}