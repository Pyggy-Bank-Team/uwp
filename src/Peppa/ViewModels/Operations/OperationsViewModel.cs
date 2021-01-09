﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Peppa.Enums;
using Peppa.ViewModels.Accounts;
using Peppa.ViewModels.Categories;
using Peppa.ViewModels.Interface;
using Peppa.ViewModels.Pagination;
using piggy_bank_uwp.Interface.Models;

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

        public async Task<CategoryItemViewModel[]> GetCategories(CategoryType categoryType)
        {
            var categories = await _model.GetCategories(GetCancellationToken());
            return categories.Where(c => c.Type == categoryType).Select(c => new CategoryItemViewModel
            {
                Id = c.Id,
                Title = c.Title,
                HexColor = c.HexColor
            }).ToArray();
        }

        public async Task<AccountItemViewModel[]> GetAccounts()
        {
            var accounts = await _model.GetAccounts(GetCancellationToken());
            return accounts.Select(a => new AccountItemViewModel
            {
                Id = a.Id,
                Title = a.Title,
                BalanceWithCurrencySymbol = $"{a.Balance} {a.Currency}"
            }).ToArray();
        }

        public async Task<OperationViewModel> GetBudgetOperation(int id)
        {
            var operation = await _model.GetBudgetOperation(id, GetCancellationToken());
            return new OperationViewModel(operation);
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
