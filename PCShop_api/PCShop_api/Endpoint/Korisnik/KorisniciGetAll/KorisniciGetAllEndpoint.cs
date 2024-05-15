using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Korisnik.KorisniciGetAll
{
    [MyAuthorization]
    [Route("KorisniciGetAll-KorisniciGetAll")]
    public class KorisniciGetAllEndpoint:MyBaseEndpoint<KorisniciGetAllRequest,KorisniciGetAllResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public KorisniciGetAllEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public override async Task<KorisniciGetAllResponse> Akcija([FromQuery]KorisniciGetAllRequest request, CancellationToken cancellationToken)
        {
            var korisniciObj = await _applicationDbContext.KorisnickiNalog
                .Select(x => new KorisniciGetAllResponseKorisnici()
                {
                    ID=x.ID,
                    KorisnickoIme = x.KorisnickoIme,
                    SlikaKorisnika = x.SlikaKorisnika,
                    isAdmin = x.isAdmin,
                    isKupac = x.isKupac,
                    isRadnik = x.isRadnik
                }).ToListAsync(cancellationToken);

            return new KorisniciGetAllResponse
            {
                Korisnici = korisniciObj
            };
        }
    }
}
