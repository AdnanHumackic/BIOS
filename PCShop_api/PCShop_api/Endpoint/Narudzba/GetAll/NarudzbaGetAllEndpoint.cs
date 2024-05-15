
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Narudzba.GetAll
{
    [MyAuthorization]
    [Route("Narudzba-GetAll")]
    public class NarudzbaGetAllEndpoint : MyBaseEndpoint<NarudzbaGetAllRequest, NarudzbaGetAllResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public NarudzbaGetAllEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet]
        public override async Task<NarudzbaGetAllResponse> Akcija([FromQuery] NarudzbaGetAllRequest request, CancellationToken cancellationToken)
        {
            var narudzbaObj = await _applicationDbContext.Narudzba
                .Select(x => new NarudzbaGetAllResponseNarudzba()
                {
                    ID = x.ID,
                    Ime = x.Ime,
                    Prezime = x.Prezime,
                    Adresa = x.Adresa,
                    BrojTelefona = x.BrojTelefona,
                    Dostavljac = x.Dostavljac,
                    UkupnaCijena = x.UkupnaCijena
                }).ToListAsync();

            return new NarudzbaGetAllResponse
            {
                Narudzbe = narudzbaObj
            };
        }
    }
}
