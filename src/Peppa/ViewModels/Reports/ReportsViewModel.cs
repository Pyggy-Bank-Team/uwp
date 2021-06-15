using System;
using System.Threading.Tasks;
using Peppa.Interface.InternalServices;
using Peppa.Interface.Models;
using Peppa.Interface.ViewModels;
using Peppa.Interface.WindowsService;

namespace Peppa.ViewModels.Reports
{
    public class ReportsViewModel : BaseViewModel, IInitialization, IReportsViewModel
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
            IsProgressShow = true;
            RaisePropertyChanged(nameof(IsProgressShow));

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

            RaisePropertyChanged(nameof(ExpenseReport));
            RaisePropertyChanged(nameof(IncomeReport));

            IsProgressShow = false;
            RaisePropertyChanged(nameof(IsProgressShow));
        }



        public ReportViewModel ExpenseReport { get; private set; }
        public ReportViewModel IncomeReport { get; private set; }

        public DateTimeOffset? From
        {
            get => _model.From;
            set
            {
                if (!value.HasValue || _model.From == value)
                    return;

                _model.From = value.Value.UtcDateTime;
                RaisePropertyChanged(nameof(From));
            }
        }

        public DateTimeOffset? To
        {
            get => _model.To;
            set
            {
                if (!value.HasValue || _model.To == value)
                    return;

                _model.To = value.Value.UtcDateTime;
                RaisePropertyChanged(nameof(To));
            }
        }

        public bool IsProgressShow { get; set; }
    }
}
