﻿namespace Peppa.Contracts.Requests
{
    public class UpdateBudgetOperationRequest
    {
        public int AccountId { get; set; }
        public int CategoryId { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
    }
}