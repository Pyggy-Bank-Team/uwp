using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Contracts.Requests;
using Peppa.Dto;
using Peppa.Enums;
using Peppa.Interface.Models;
using Peppa.Interface.Services;

namespace Peppa.Models
{
    public class ReportModel : IReportModel
    {
        private readonly IReportService _service;

        public ReportModel(IReportService service)
        {
            _service = service;
        }
        
        public async Task<IEnumerable<ChartByCategories>> GetChartByCategories(CategoryType type, DateTime from, DateTime to, CancellationToken token)
        {
            if (_service.IsAuthorized)
            {
                var request = new GetChartByCategoriesRequest
                {
                    Type = type,
                    From = from,
                    To = to
                };

                var response = await _service.GetChartByCategories(request, token);
                return response.Select(c => new ChartByCategories
                {
                    Amount = c.Amount,
                    CategoryId = c.CategoryId,
                    CategoryTitle = c.CategoryTitle,
                    CategoryHexColor = c.CategoryHexColor
                });
            }

            return null;
        }
    }
}