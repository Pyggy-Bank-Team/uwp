namespace Peppa.Contracts.Requests
{
    public class UserRequest
    {        
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CurrencyBase { get; set; }
        public string Email { get; set; }
    }
}