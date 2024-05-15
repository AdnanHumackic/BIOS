using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Data.Models;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;
using PCShop_api.SignalR;
using System.Threading;

namespace PCShop_api.Endpoint.AuthEndpoints.Login
{
    [Route("Auth")]
    public class AuthLoginEndpoint:MyBaseEndpoint<AuthLoginRequest, MyAuthInfo>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHubContext<SignalRHub> _hubContext;

        public AuthLoginEndpoint(ApplicationDbContext applicationDbContext, IHubContext<SignalRHub> hubContext)
        {
            _applicationDbContext = applicationDbContext;
            _hubContext = hubContext;
        }
        [HttpPost("Login")]
        public override async Task<MyAuthInfo> Akcija([FromBody]AuthLoginRequest request, CancellationToken cancellationToken)
        {
            //1- provjera logina
            Data.Models.KorisnickiNalog? logiraniKorisnik = await _applicationDbContext.KorisnickiNalog
                .FirstOrDefaultAsync(k =>
                    k.KorisnickoIme == request.KorisnickoIme && k.Lozinka == request.Lozinka);

            if (logiraniKorisnik == null)
            {
                //pogresan username i password
                return new MyAuthInfo(null);
            }

            string? twoFKey = null;


            string randomString = TokenGenerator.Generate();

            //3- dodati novi zapis u tabelu AutentifikacijaToken za logiraniKorisnikId i randomString
            var noviToken = new AutentifikacijaToken()
            {
                ipAdresa = Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
                Vrijednost = randomString,
                korisnickiNalog = logiraniKorisnik,
                vrijemeEvidentiranja = DateTime.Now,
                TwoFKey = twoFKey
            };

            _applicationDbContext.Add(noviToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            await _hubContext.Groups.AddToGroupAsync(request.SignalRConnectionID, noviToken.korisnickiNalog.KorisnickoIme);
            //4- vratiti token string
            return new MyAuthInfo(noviToken);
        }
    }
}
