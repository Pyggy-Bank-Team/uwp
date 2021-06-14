using System.Collections.Generic;
using System.Linq;
using Peppa.Dto;
using Peppa.Interface.Models.Reports;

namespace Peppa.Models.Reports
{
    public class ReportModel : IReportModel
    {
        public ReportModel(IEnumerable<ItemReport> report)
        {
            List = new List<ItemReport>(report);
            TotalAmount = List.Sum(i => i.Amount);
        }
        
        public double TotalAmount { get; }
        public List<ItemReport> List { get; }
    }
}