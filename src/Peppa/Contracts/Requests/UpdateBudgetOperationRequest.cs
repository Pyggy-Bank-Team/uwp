using System;

namespace Peppa.Contracts.Requests
{
    public class UpdateBudgetOperationRequest
    {
        public int AccountId { get; set; }
        public int CategoryId { get; set; }
        public double Amount { get; set; }
        public string Comment { get; set; }
        public DateTime OperationDate { get; set; }
    }
}
