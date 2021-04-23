namespace Peppa.Contracts.Requests
{
    public class UpdateTransferOperationRequest
    {
        public int From { get; set; }
        public int To { get; set; }
        public double Amount { get; set; }
        public string Comment { get; set; }
    }
}
