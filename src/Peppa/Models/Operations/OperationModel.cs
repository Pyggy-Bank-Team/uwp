using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Context.Entities;
using Peppa.Contracts.Requests;
using Peppa.Enums;
using Peppa.Helpers;
using Peppa.Interface;
using Peppa.Interface.Models;
using Peppa.Interface.Services;
using Account = Peppa.Dto.Account;
using Category = Peppa.Dto.Category;

namespace Peppa.Models.Operations
{
    public class OperationModel : BaseModel, IOperationModel
    {
        private readonly Operation _operation;
        private readonly IPiggyRepository _repository;
        private readonly IOperationService _service;
        private readonly IAccountService _accountService;
        private readonly ICategoryService _categoryService;

        public OperationModel(Operation operation, IPiggyRepository repository, IOperationService service, IAccountService accountService, ICategoryService categoryService, bool isNew = false)
        {
            _operation = operation;
            _repository = repository;
            _service = service;
            _accountService = accountService;
            _categoryService = categoryService;
            Accounts = new List<Account>();
            Categories = new List<Category>();

            Amount = operation.Amount;
            Type = operation.Type;
            Comment = operation.Comment;
            OperationDate = operation.CreatedOn;
            CategoryType = operation.CategoryType ?? CategoryType.Undefined;
            CategoryHexColor = operation.CategoryHexColor ?? "#FFFFFF";
            CategoryTitle = operation.CategoryTitle;
            AccountTitle = operation.AccountTitle;
            ToAccountTitle = operation.ToTitle;
            Symbol = operation.Symbol;
            Id = operation.Id;
            IsNew = isNew;
        }

        public double Amount { get; set; }
        public OperationType Type { get; set; }
        public string Comment { get; set; }
        public DateTime OperationDate { get; set; }
        public int AccountId { get; set; }
        public int CategoryId { get; set; }
        public int ToAccountId { get; set; }
        public int Id { get; set; }
        public CategoryType CategoryType { get; }
        public string CategoryHexColor { get; }
        public string CategoryTitle { get; }
        public string AccountTitle { get; }
        public string ToAccountTitle { get; }
        public string Symbol { get; }
        public List<Account> Accounts { get; private set; }
        public List<Category> Categories { get; set; }
        public bool IsNew { get; }

        public async Task UpdateData(CancellationToken token)
        {
            if (IsNew)
                return;

            switch (Type)
            {
                case OperationType.Budget:
                {
                    var response = await _service.GetBudgetOperation(Id, token);
                    AccountId = response.AccountId;
                    CategoryId = response.CategoryId;
                    break;
                }
                case OperationType.Transfer:
                {
                    var response = await _service.GetTransferOperation(Id, token);
                    AccountId = response.FromId;
                    ToAccountId = response.ToId;
                    break;
                }
            }
        }

        public async Task Save(CancellationToken token)
        {
            switch (Type)
            {
                case OperationType.Budget:
                {
                    var request = new CreateBudgetOperationRequest
                    {
                        Amount = Amount,
                        Comment = Comment,
                        OperationDate = OperationDate,
                        CategoryId = CategoryId,
                        AccountId = AccountId
                    };

                    await _service.CreateBudgetOperation(request, token);
                    break;
                }
                case OperationType.Transfer:
                {
                    var request = new CreateTransferOperationRequest
                    {
                        Amount = Amount,
                        Comment = Comment,
                        OperationDate = OperationDate,
                        From = AccountId,
                        To = ToAccountId
                    };

                    await _service.CreateTransferOperation(request, token);
                    break;
                }
            }
        }

        public async Task Update(CancellationToken token)
        {
            switch (Type)
            {
                case OperationType.Budget:
                {
                    var request = new UpdateBudgetOperationRequest
                    {
                        Amount = Amount,
                        Comment = Comment,
                        CategoryId = CategoryId,
                        AccountId = AccountId
                    };

                    await _service.UpdateBudgetOperation(Id, request, token);
                    break;
                }
                case OperationType.Transfer:
                {
                    var request = new UpdateTransferOperationRequest()
                    {
                        Amount = Amount,
                        Comment = Comment,
                        From = AccountId,
                        To = ToAccountId
                    };

                    await _service.UpdateTransferOperation(Id, request, token);
                    break;
                }
            }
        }

        public async Task Delete(CancellationToken token)
        {
            switch (Type)
            {
                case OperationType.Budget:
                {
                    await _service.DeleteBudgetOperation(Id, token);
                    break;
                }
                case OperationType.Transfer:
                {
                    await _service.DeleteTransferOperation(Id, token);
                    break;
                }
            }
        }

        public async Task UpdateAccounts(bool showArchivedAccounts, CancellationToken token)
        {
            var response = await _accountService.GetAccounts(showArchivedAccounts, token);
            if (response != null)
            {
                Accounts.Clear();
                foreach (var account in response)
                    Accounts.Add(new Account
                    {
                        Id = account.Id,
                        Title = account.Title,
                        BalanceWithCurrency = $"{account.Balance} {CurrencyHelper.GetSymbol(account.Currency)}",
                        Currency = CurrencyHelper.GetSymbol(account.Currency)
                    });
            }
        }

        public async Task UpdateCategories(bool showArchivedCategories, CancellationToken token)
        {
            var response = await _categoryService.GetCategories(showArchivedCategories, token);
            if (response != null)
            {
                Categories.Clear();
                foreach (var category in response)
                    Categories.Add(new Category {Id = category.Id, Title = category.Title, HexColor = category.HexColor, Type =  category.Type});
            }
        }
    }
}