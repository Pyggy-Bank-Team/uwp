using Peppa.Enums;

namespace Peppa.Contracts.Responses
{
    public class AccountResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public AccountType Type { get; set; }
        public string Currency { get; set; }
        public double Balance { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsArchived { get; set; }
    }
}