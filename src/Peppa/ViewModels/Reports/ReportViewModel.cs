using System.Collections.Generic;
using Peppa.Dto;
using Peppa.Interface.Models.Reports;

namespace Peppa.ViewModels.Reports
{
    public class ReportViewModel
    {
        private readonly IReportModel _model;

        public ReportViewModel(IReportModel model, string title)
        {
            _model = model;
            Title = title;
        }

        public string Title { get; }
        public double TotalAmount => _model.TotalAmount;
        public List<ItemReport> List => _model.List;
    }
}