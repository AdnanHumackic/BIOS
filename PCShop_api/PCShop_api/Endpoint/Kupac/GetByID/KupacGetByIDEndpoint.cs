using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Kupac.GetByID
{
    [MyAuthorization]
    [Route("Kupac-GetByID")]
    public class KupacGetByIDEndpoint:MyBaseEndpoint<KupacGetByIDRequest, KupacGetByIDResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _authService;

        public KupacGetByIDEndpoint(ApplicationDbContext applicationDbContext, MyAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
        }

        [HttpGet]
        public override async Task<KupacGetByIDResponse> Akcija([FromQuery]KupacGetByIDRequest request, CancellationToken cancellationToken)
        {
            var kupac = await _applicationDbContext.KorisnickiNalog.Where(x => x.ID == request.ID
            /*&& _authService.IsKupac()*/).FirstOrDefaultAsync(cancellationToken);

            return new KupacGetByIDResponse
            {
                ID=kupac.ID,
                KorisnickoIme = kupac.KorisnickoIme,
                Lozinka = kupac.Lozinka,
                Ime = kupac.Kupac.Ime,
                Prezime = kupac.Kupac.Prezime,
                Drzava = kupac.Kupac.DrzavaID,
                SlikaKorisnika=kupac.SlikaKorisnika,
                Email=kupac.Kupac.Email
            };
        }
    }
}
