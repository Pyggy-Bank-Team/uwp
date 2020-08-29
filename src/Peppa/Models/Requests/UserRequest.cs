namespace piggy_bank_uwp.Models.Requests
{
    public class UserRequest
    {
        public static UserRequest GetTempUserRequest()
            => new UserRequest
            {
                UserName = "denis",
                Password = "qwerty123"
            };
        
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CurrencyBase { get; set; }
    }
}