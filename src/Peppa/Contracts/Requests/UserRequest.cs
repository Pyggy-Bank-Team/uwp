namespace piggy_bank_uwp.Contracts.Requests
{
    public class UserRequest
    {
        public static UserRequest GetTempUserRequest()
            => new UserRequest
            {
                UserName = "denis",
                Password = "qwerty123",
                CurrencyBase = "RUB"
            };
        
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CurrencyBase { get; set; }
    }
}