﻿using System.Threading.Tasks;
using Peppa.Contracts.Requests;
using Peppa.Contracts.Responses;
using Peppa.Workers;
using Peppa.Interface.Services;
using Peppa.Models;

namespace Peppa.ViewModels.Users
{
    public class UserViewModel : BaseViewModel
    {
        private readonly IUserService _userService;

        public UserViewModel(IUserService userService)
            => _userService = userService;

        public async Task OnLogin(string userName, string password)
        {
            var request = new GetTokenRequest
            {
                UserName = userName,
                Password = password
            };

            var accessToken = await _userService.GetAccessToken(request, GetCancellationToken());

            if (accessToken != null)
            {
                SaveAccessToken(accessToken);
            }
        }

        public Task<RegitrationResult> OnRegistration(string userName, string password, string currency)
        {
            //TODO: Add main currency
            var request = new UserRequest
            {
                UserName = userName,
                Password = password,
                CurrencyBase = currency
            };

            return _userService.RegistrationUser(request, GetCancellationToken());
        }

        public void SaveAccessToken(AccessTokenResponse accessToken)
        {
            SettingsWorker.Current.SaveValue(Constants.AccessToken, accessToken.AccessToken);
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

        public Task<CurrencyResponse[]> GetCurrencies()
            => _userService.GetAvailableCurrencies(GetCancellationToken());

        public string Token
            => (string)SettingsWorker.Current.GetValue(Constants.AccessToken);

        public string UserName
            => (string)SettingsWorker.Current.GetValue(Constants.UserName);
    }
}