﻿using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Interface.Services;
using piggy_bank_uwp.Contracts;
using piggy_bank_uwp.Contracts.Responses;

namespace Peppa.Services.PiggyService
{
    public class OperationService : PiggyServiceBase, IOperationService
    {
        public OperationService(IHttpClientFactory httpClientFactory) 
            : base(httpClientFactory) { }
        
        public Task<PageResult<OperationResponse>> GetOperations(CancellationToken token)
        {
            return Get<PageResult<OperationResponse>>("operations", token);
        }
    }
}