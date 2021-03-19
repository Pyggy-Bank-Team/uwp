using System;

namespace Peppa.Contracts.Responses
{
    public class ChartByExpenseDaysResponse
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}