﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Peppa.Enums;
using Peppa.Interface.Models;
using Peppa.ViewModels.Accounts;
using Peppa.ViewModels.Categories;
using Peppa.ViewModels.Interface;
using Peppa.ViewModels.Pagination;

namespace Peppa.ViewModels.Operations
{
    public class OperationsViewModel : BaseViewModel, IBaseViewModel
    {
        private readonly IOperationsModel _model;

        public OperationsViewModel(IOperationsModel model)
            => _model = model;

        public async Task Initialization()
        {
            var pageResult = await _model.GetOperations(CurrentPage, GetCancellationToken());
            if (pageResult != null)
            {
                List = new ObservableCollection<ListItemViewModel>(pageResult.Result.OrderByDescending(o => o.CreatedOn).Select(o => new ListItemViewModel(o)));
                CurrentPage = pageResult.CurrentPage;
                TotalPages = pageResult.TotalPages;
                Pagination = new ObservableCollection<PaginationItemViewModel>(Enumerable.Range(1, TotalPages).Select(i => new PaginationItemViewModel { Number = i }));
                RaisePropertyChanged(nameof(List));
                RaisePropertyChanged(nameof(Pagination));
            }
        }

        public async Task DoAction(ListItemViewModel operation)
        {
            switch (operation.Action)
            {
                case ActionType.Save when operation.IsNew:

                    if (operation.IsBudget)
                        await _model.CreateBudgetOperation(operation, GetCancellationToken());
                    else
                        await _model.CreateTransferOperation(operation, GetCancellationToken());
                    
                    break;
                case ActionType.Save when !operation.IsNew:

                    if (operation.IsBudget)
                        await _model.UpdateBudgetOperation(operation, GetCancellationToken());
                    else
                        await _model.UpdateTransferOperation(operation, GetCancellationToken());
                    
                    break;
                case ActionType.Delete:
                    break;
                default:
                    //TODO: Do nothing
                break;
            }
        }

        public async Task<CategoryItemViewModel[]> GetCategories(bool all, CategoryType categoryType)
        {
            var categories = await _model.GetCategories(all, GetCancellationToken());
            return categories.Where(c => c.Type == categoryType).Select(c => new CategoryItemViewModel
            {
                Id = c.Id,
                Title = c.Title,
                HexColor = c.HexColor
            }).ToArray();
        }

        public async Task<AccountItemViewModel[]> GetAccounts(bool all)
        {
            var accounts = await _model.GetAccounts(all, GetCancellationToken());
            return accounts.Select(a => new AccountItemViewModel
            {
                Id = a.Id,
                Title = a.Title,
                BalanceWithCurrencySymbol = $"{a.Balance} {a.Currency}"
            }).ToArray();
        }

        public async Task<BudgetOperationViewModel> GetBudgetOperation(int id)
        {
            var operation = await _model.GetBudgetOperation(id, GetCancellationToken());
            return new BudgetOperationViewModel{AccountId = operation.AccountId.Value, CategoryId = operation.CategoryId.Value};
        }

        public async Task<TransferOperationViewModel> GetTransferOperation(int id)
        {
            var operation = await _model.GetTransferOperation(id, GetCancellationToken());
            return new TransferOperationViewModel{FromId = operation.AccountId.Value, ToId = operation.ToId.Value};
        }

        public void Finalization()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<ListItemViewModel> List { get; private set; }

        public ObservableCollection<PaginationItemViewModel> Pagination { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalPages { get; set; }
    }
}
