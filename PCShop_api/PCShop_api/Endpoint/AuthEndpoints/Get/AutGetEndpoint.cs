using Microsoft.AspNetCore.Mvc;
using PCShop_api.Data;
using PCShop_api.Data.Models;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.AuthEndpoints.Get
{
    [Route("Auth")]
    public class AutGetEndpoint:MyBaseEndpoint<NoRequest, MyAuthInfo>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _authService;
        public AutGetEndpoint(ApplicationDbContext applicationDbContext, MyAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
        }

        [HttpPost("Get")]
        public override async Task<MyAuthInfo> Akcija([FromBody]NoRequest request, CancellationToken cancellationToken)
        {
            AutentifikacijaToken? autentifikacijaToken = _authService.GetAuthInfo().autentifikacijaToken;

            return new MyAuthInfo(autentifikacijaToken);
        }


    }
}
