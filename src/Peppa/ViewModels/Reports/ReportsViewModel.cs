using System;
using System.Linq;
using System.Threading.Tasks;
using Peppa.Enums;
using Peppa.Interface.InternalServices;
using Peppa.Interface.Models;
using Peppa.Interface.ViewModels;
using Peppa.Interface.WindowsService;

namespace Peppa.ViewModels.Reports
{
    public class ReportsViewModel : BaseViewModel, IInitialization
    {
        private readonly IReportsModel _model;
        private readonly IToastService _toastService;
        private readonly ILocalizationService _localizationService;

        public ReportsViewModel(IReportsModel model, IToastService toastService, ILocalizationService localizationService)
        {
            _model = model;
            _toastService = toastService;
            _localizationService = localizationService;
        }

        public async Task Initialization()
        {
            try
            {
                await _model.UpdateReports(GetCancellationToken());
            }
            catch
            {
                _toastService.ShowNotification("SoS", _localizationService.GetTranslateByKey(Localization.OopsError));
            }

            ExpenseReport = new ReportViewModel(_model.ExpenseReport, _localizationService.GetTranslateByKey(Localization.Expense));
            IncomeReport = new ReportViewModel(_model.IncomeReport, _localizationService.GetTranslateByKey(Localization.Income));
        }
        
        
        
        public ReportViewModel ExpenseReport { get; private set; }
        public ReportViewModel IncomeReport { get; private set; }

        public DateTime From
        {
            get => _model.From;
            set
            {
                if (_model.From == value)
                    return;

                _model.From = value;
                RaisePropertyChanged(nameof(From));
            }
        }

        public DateTime To
        {
            get => _model.To;
            set
            {
                if (_model.To == value)
                    return;

                _model.To = value;
                RaisePropertyChanged(nameof(To));
            }
        }
    }
}
