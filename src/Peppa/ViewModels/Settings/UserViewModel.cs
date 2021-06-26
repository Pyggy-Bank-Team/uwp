using System.Collections.Generic;
using Peppa.Enums;
using Peppa.Interface.Models.Settings;

namespace Peppa.ViewModels.Settings
{
    public class UserViewModel
    {
        public UserViewModel(ISettingsModel model)
        {
            UserName = model.Login;
            Email = model.Email;
            Currencies = new List<string> {model.Currency};
        }

        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Currencies { get; set; }
        public DialogResult Result { get; set; }
    }
}