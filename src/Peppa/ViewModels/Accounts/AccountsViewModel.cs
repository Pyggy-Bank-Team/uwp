using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Peppa.Enums;
using Peppa.Interface.InternalServices;
using Peppa.ViewModels.Interface;
using Peppa.Interface.Models.Accounts;
using Peppa.Interface.WindowsService;

namespace Peppa.ViewModels.Accounts
{
    public class AccountsViewModel : BaseViewModel, IInitialization
    {
        private readonly IAccountsModel _model;
        private readonly IToastService _toastService;
        private readonly ILocalizationService _localizationService;

        public AccountsViewModel(IAccountsModel model, IToastService toastService, ILocalizationService localizationService)
        {
            _model = model;
            _toastService = toastService;
            _localizationService = localizationService;
            List = new ObservableCollection<AccountViewModel>();
        }

        public async Task Initialization()
        {
            
        }

        //TODO Split on two separated methods
        internal async Task UpdateData()
        {
            switch (SelectedItem?.Action)
            {
                case DialogResult.Save when SelectedItem?.IsNew == true || SelectedItem?.IsSynchronized == false:
                    await _model.CreatedAccount(SelectedItem.MakeAccountEntity(), GetCancellationToken());
                    break;
                case DialogResult.Save when SelectedItem?.IsNew == false:
                    await _model.UpdateAccount(SelectedItem.MakeAccountEntity(), GetCancellationToken());
                    break;
                case DialogResult.Delete:
                    await _model.DeleteAccount(SelectedItem.Id, GetCancellationToken());
                    break;
            }
        }

        public void OnAddOperationClick(object sender, RoutedEventArgs e)
        {
            
        }

        public string TotalBalanceTitle
        {
            get
            {
                if (List.Count > 0)
                {
                    var sum = List.Where(l => !l.IsArchived).Sum(l => l.Balance);
                    return $"{sum} {List.FirstOrDefault()?.Currency}";
                }

                return string.Empty;
            }
        }

        public ObservableCollection<AccountViewModel> List { get; private set; }

        public AccountViewModel SelectedItem { get; set; }
        
        public bool IsProgressShow { get; set; }
    }
}