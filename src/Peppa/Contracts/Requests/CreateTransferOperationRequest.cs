using System;

namespace Peppa.Contracts.Requests
{
    public class CreateTransferOperationRequest
    {
        public DateTime OperationDate { get; set; }
        public long From { get; set; }
        public long To { get; set; }
        public long Amount { get; set; }
        public string Comment { get; set; }
    }
}