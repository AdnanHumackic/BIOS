using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Helper;

namespace PCShop_api.Endpoint.Radnik.GetAll
{
    [Route("Radnik-GetAll")]
    public class RadnikGetAllEndpoint:MyBaseEndpoint<RadnikGetAllRequest,RadnikGetAllResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RadnikGetAllEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet]
        public override async Task<RadnikGetAllResponse> Akcija([FromQuery] RadnikGetAllRequest request, CancellationToken cancellationToken)
        {
            var radnikObj = await _applicationDbContext.Radnik
                .Select(x => new RadnikGetAllResponseRadnik()
                {
                    ID = x.ID,
                    Ime = x.Ime,
                    Prezime = x.Prezime,
                    DatumRodjenja = x.DatumRodjenja,
                    DatumZaposlenja = x.DatumZaposlenja,
                    KorisnickoIme = x.KorisnickoIme
                }).ToListAsync(cancellationToken);

            return new RadnikGetAllResponse
            {
                Radnik = radnikObj
            };
        }
    }
}
