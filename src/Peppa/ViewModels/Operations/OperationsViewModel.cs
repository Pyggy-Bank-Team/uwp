using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Peppa.Enums;
using Peppa.ViewModels.Accounts;
using Peppa.ViewModels.Categories;
using Peppa.ViewModels.Interface;
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
            var operations = await _model.GetOperations(GetCancellationToken());
            if (operations != null)
            {
                List = new ObservableCollection<ListItemViewModel>(operations.OrderBy(o => o.CreatedOn).Select(o => new ListItemViewModel(o)));
                RaisePropertyChanged(nameof(List));
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

        public OperationViewModel SelectedItem { get; set; }
    }
}
