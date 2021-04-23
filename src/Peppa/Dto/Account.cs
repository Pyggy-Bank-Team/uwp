namespace Peppa.Dto
{
    public class Account
    {
        public int Id { get; set; }
        public string  Title { get; set; }
        public string BalanceWithCurrency { get; set; }
        public string Currency { get; set; }
    }
}