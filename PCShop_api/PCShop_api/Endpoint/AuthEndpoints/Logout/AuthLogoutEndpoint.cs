using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using PCShop_api.Data;
using PCShop_api.Data.Models;
using PCShop_api.Endpoint.AuthEndpoints.Login;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;
using PCShop_api.SignalR;
using System.Threading;

namespace PCShop_api.Endpoint.AuthEndpoints.Logout
{
    [Route("Auth")]
    public class AuthLogoutEndpoint:MyBaseEndpoint<AuthLogoutRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _authService;
        private readonly IHubContext<SignalRHub> _hubContext;

        public AuthLogoutEndpoint(ApplicationDbContext applicationDbContext, MyAuthService authService,
            IHubContext<SignalRHub>hubContext)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
            _hubContext = hubContext;
        }

        [HttpPost("Logout")]

        public override async Task<NoResponse> Akcija([FromBody]AuthLogoutRequest request, CancellationToken cancellationToken)
        {
            AutentifikacijaToken? autentifikacijaToken = _authService.GetAuthInfo().autentifikacijaToken;

            if (autentifikacijaToken == null)
                return new NoResponse();

            await _hubContext.Groups.AddToGroupAsync(
                request.SignalRConnectionID,
                autentifikacijaToken.korisnickiNalog.KorisnickoIme, cancellationToken);

            _applicationDbContext.Remove(autentifikacijaToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return new NoResponse();
        }
    }
}
