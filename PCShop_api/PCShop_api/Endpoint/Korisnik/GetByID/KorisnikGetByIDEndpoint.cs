using Microsoft.AspNetCore.Mvc;
using PCShop_api.Data;
using PCShop_api.Helper.Auth;
using PCShop_api.Helper;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data.Models;

namespace PCShop_api.Endpoint.Korisnik.GetByID
{
    [MyAuthorization]
    [Route("Korisnik-GetByID")]
    public class KorisnikGetByIDEndpoint : MyBaseEndpoint<KorisnikGetByIDRequest, KorisnikGetByIDResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _authService;

        public KorisnikGetByIDEndpoint(ApplicationDbContext applicationDbContext, MyAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
        }
        [HttpGet]
        public override async Task<KorisnikGetByIDResponse> Akcija([FromQuery] KorisnikGetByIDRequest request, CancellationToken cancellationToken)
        {
            
            var korisnik = await _applicationDbContext.KorisnickiNalog.Where(x=>x.ID==request.ID
            && _authService.JelLogiran()).FirstOrDefaultAsync(cancellationToken);

            return new KorisnikGetByIDResponse
            {
                KorisnickoIme = korisnik.KorisnickoIme!,
                Slika = korisnik.SlikaKorisnika!,
            };
        }
    }
}
