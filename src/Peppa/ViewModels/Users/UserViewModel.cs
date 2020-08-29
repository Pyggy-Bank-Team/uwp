﻿using System.Threading.Tasks;
using piggy_bank_uwp.Interface;
using piggy_bank_uwp.Models.Requests;
using piggy_bank_uwp.Models.Responses;
using piggy_bank_uwp.Workers;

namespace piggy_bank_uwp.ViewModels.Users
{
    public class UserViewModel
    {
        private readonly IUserService _userService;

        public UserViewModel(IUserService userService)
            => _userService = userService;

        public async Task OnLogin(string userName, string password)
        {
            var request = new UserRequest
            {
                UserName = userName,
                Password = password
            };

            var accessToken = await _userService.GetAccessToken(request);

            if (accessToken != null)
            {
                SaveAccessToken(accessToken);
            }
        }

        public async Task OnRegistration(string userName, string password, string currency)
        {
            //TODO: Add main currency
            var request = new UserRequest
            {
                UserName = userName,
                Password = password,
                CurrencyBase = currency
            };

            var result = await _userService.RegistrationUser(request);
        }

        public void SaveAccessToken(AccessTokenResponse accessToken)
        {
            SettingsWorker.Current.SaveValue(Constants.AccessToken, accessToken.AccessToken);
            SettingsWorker.Current.SaveValue(Constants.RefreshToken, accessToken.RefreshToken);
        }

        public void SaveUserName(string userName)
        {
            SettingsWorker.Current.SaveValue(Constants.UserName, userName);
        }

        public void RemovedSaveData()
        {
            SettingsWorker.Current.RemoveValue(Constants.AccessToken);
            SettingsWorker.Current.RemoveValue(Constants.RefreshToken);
            SettingsWorker.Current.RemoveValue(Constants.UserName);
        }

        public string Token
            => (string)SettingsWorker.Current.GetValue(Constants.AccessToken);

        public string UserName
            => (string)SettingsWorker.Current.GetValue(Constants.UserName);
    }
}