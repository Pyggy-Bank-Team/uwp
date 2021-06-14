using System.Collections.Generic;
using System.Linq;
using Peppa.Dto;

namespace Peppa.Models.Reports
{
    public class ReportModel
    {
        public ReportModel(IEnumerable<ItemReport> report)
        {
            List = new List<ItemReport>(report);
            TotalAmount = List.Sum(i => i.Amount);
        }
        
        public double TotalAmount { get; set; }
        public List<ItemReport> List { get; set; }
    }
}