using System.Collections.Generic;
using Peppa.Dto;

namespace Peppa.Interface.Models.Reports
{
    public interface IReportModel
    {
        double TotalAmount { get; }
        List<ItemReport> List { get; }
    }
}