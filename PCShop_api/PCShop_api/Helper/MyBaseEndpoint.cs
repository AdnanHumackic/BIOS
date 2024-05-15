using Azure.Core;
using Azure;
using Microsoft.AspNetCore.Mvc;

namespace PCShop_api.Helper
{
    public abstract class MyBaseEndpoint<TRequest, TResponse> : ControllerBase
    {
        public abstract Task<TResponse> Akcija(TRequest request, CancellationToken cancellationToken);
    }
}
