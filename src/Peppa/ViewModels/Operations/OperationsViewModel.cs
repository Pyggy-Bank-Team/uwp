using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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
            var operations = await _model.GetOperations(GetToken());
            if (operations != null)
            {
                List = new ObservableCollection<OperationViewModel>(operations.OrderBy(o => o.CreatedOn).Select(o => new OperationViewModel(o)));
                RaisePropertyChanged(nameof(List));
            }
        }

        public void Finalization()
        {
            throw new NotImplementedException();
        }
        
        public ObservableCollection<OperationViewModel> List { get; private set; }
        
        public OperationViewModel SelectedItem { get; set; }
    }
}
