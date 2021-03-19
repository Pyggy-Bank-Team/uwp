using System;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Interface;
using Peppa.Interface.Services;
using System.Collections.Generic;
using Peppa.Context.Entities;
using Peppa.Dto;
using System.Linq;
using Peppa.Contracts.Requests;
using Peppa.Interface.Models;

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
                        CreatedOn = o.Date,
                        Comment = o.Comment
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
                            CreatedOn = o.Date,
                            Comment = o.Comment
                        }).ToArray()
                    };
                }
            }

            return null;
            //return await _repository.GetOperations(token);
        }

        public Task<Account[]> GetAccounts(bool all, CancellationToken token)
           => _repository.GetAccounts(token, all);

        public Task<Category[]> GetCategories(bool all, CancellationToken token)
            => _repository.GetCategories(token, all);

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

        public async Task<Operation> GetTransferOperation(int operationId, CancellationToken token)
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

        public async Task CreateBudgetOperation(Operation operation, CancellationToken token)
        {
            if (_service.IsAuthorized)
            {
                var request = new CreateBudgetOperationRequest
                {
                    Amount = operation.Amount,
                    AccountId = operation.AccountId.Value,
                    CategoryId = operation.CategoryId.Value,
                    OperationDate = operation.CreatedOn,
                    Comment = operation.Comment
                };

                var result = await _service.CreateBudgetOperation(request, token);
                
                //TODO: save to db
            }
        }

        public async Task CreateTransferOperation(Operation operation, CancellationToken token)
        {
            if (_service.IsAuthorized)
            {
                var request = new CreateTransferOperationRequest
                {
                    Amount = operation.Amount,
                    From = operation.AccountId.Value,
                    To = operation.ToId.Value,
                    OperationDate = operation.CreatedOn,
                    Comment = operation.Comment
                };

                var result = await _service.CreateTransferOperation(request, token);
                
                //TODO: save to db
            }
        }

        public async Task UpdateBudgetOperation(Operation operation, CancellationToken token)
        {
            if (_service.IsAuthorized)
            {
                var request = new UpdateBudgetOperationRequest
                {
                    AccountId = operation.AccountId.Value,
                    Amount = operation.Amount,
                    CategoryId = operation.CategoryId.Value,
                    Comment = operation.Comment
                };

                await _service.UpdateBudgetOperation(operation.Id, request, token);
            }
        }

        public async Task UpdateTransferOperation(Operation operation, CancellationToken token)
        {
            if (_service.IsAuthorized)
            {
                var request = new UpdateTransferOperationRequest
                {
                    From = operation.AccountId.Value,
                    Amount = operation.Amount,
                    To = operation.ToId.Value,
                    Comment = operation.Comment
                };

                await _service.UpdateTransferOperation(operation.Id, request, token);
            }
        }
    }
}