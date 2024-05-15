using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Endpoint.Korpa.GetByID;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Narudzba.GetByID
{
    [MyAuthorization]
    [Route("Narudzba-GetByID")]
    public class NarudzbaGetByIDKorisnik : MyBaseEndpoint<NarudzbaGetByIDRequest,NarudzbaGetByIDResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;
        public NarudzbaGetByIDKorisnik(ApplicationDbContext applicationDbContext, MyAuthService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
        }
        [HttpGet]
        public override async Task<NarudzbaGetByIDResponse> Akcija([FromQuery] NarudzbaGetByIDRequest request, CancellationToken cancellationToken)
        {
            var narudzba = await _applicationDbContext.Narudzba.Where(x => x.EvidentiraoKorisnikId == request.ID).Select(x => new NarudzbaGetByIdReponseNarudzba()
            {
                ID = x.ID,
                Adresa = x.Adresa,
                BrojTelefona = x.BrojTelefona,
                Dostavljac = x.Dostavljac,
                Ime = x.Ime,
                Prezime = x.Prezime,
                UkupnaCijena = x.UkupnaCijena
            }).ToListAsync(cancellationToken);

            return new NarudzbaGetByIDResponse
            {
                Narudzba = narudzba
            };
        }
    }
}
