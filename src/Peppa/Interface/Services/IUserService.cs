﻿using System.Threading;
using System.Threading.Tasks;
using Peppa.Contracts.Requests;
using Peppa.Contracts.Responses;
using Peppa.Models;

namespace Peppa.Interface.Services
{
    public interface IUserService : IAuthorization
    {
        Task<RegitrationResult> RegistrationUser(UserRequest request);

        Task<AccessTokenResponse> GetAccessToken(GetTokenRequest request);

        Task<CurrencyResponse[]> GetAvailableCurrencies(CancellationToken token);
    }
}
