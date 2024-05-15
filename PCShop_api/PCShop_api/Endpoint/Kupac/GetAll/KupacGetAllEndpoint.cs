using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Kupac.GetAll
{
    [MyAuthorization]
    [Route("Kupac-GetAll")]
    public class KupacGetAllEndpoint : MyBaseEndpoint<KupacGetAllRequest, KupacGetAllResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public KupacGetAllEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet]
        public override async Task<KupacGetAllResponse> Akcija([FromQuery] KupacGetAllRequest request, CancellationToken cancellationToken)
        {
            var kupacObj = await _applicationDbContext.Kupac
                .Select(X => new KupacGetAllReponseKupac()
                {
                    ID = X.ID,
                    Ime = X.Ime,
                    Prezime = X.Prezime,
                    KorisnickoIme = X.KorisnickoIme,
                    Drzava = X.DrzavaID,
                    DatumRodjenja = X.DatumRodjenja,
                    Lozinka = X.Lozinka,
                    Email=X.Email
                }).ToListAsync(cancellationToken: cancellationToken);

            return new KupacGetAllResponse
            {
                Kupac = kupacObj
            };
        }
    }
}
