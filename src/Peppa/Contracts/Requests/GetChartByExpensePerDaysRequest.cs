using System;

namespace Peppa.Contracts.Requests
{
    public class GetChartByExpensePerDaysRequest
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}