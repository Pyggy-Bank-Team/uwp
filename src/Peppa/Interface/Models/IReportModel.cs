using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Dto;
using Peppa.Enums;

namespace Peppa.Interface.Models
{
    public interface IReportModel
    {
        Task<IEnumerable<ChartByCategories>> GetChartByCategories(CategoryType type, DateTime from, DateTime to, CancellationToken token);
    }
}