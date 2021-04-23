using System;

namespace Peppa.Contracts.Requests
{
    public class CreateTransferOperationRequest
    {
        public DateTime OperationDate { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public double Amount { get; set; }
        public string Comment { get; set; }
    }
}