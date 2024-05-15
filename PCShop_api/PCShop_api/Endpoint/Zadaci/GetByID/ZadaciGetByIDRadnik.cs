using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Endpoint.Zadaci.GetAll;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;
using PCShop_api.SignalR;

namespace PCShop_api.Endpoint.Zadaci.GetByID
{
    [MyAuthorization]
    [Route("Zadaci-GetByID")]
    public class ZadaciGetByIDRadnik:MyBaseEndpoint<ZadaciGetByIDRequest,ZadaciGetByIDResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;

        public ZadaciGetByIDRadnik(ApplicationDbContext applicationDbContext, MyAuthService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
        }
        [HttpGet]
        public override async Task<ZadaciGetByIDResponse> Akcija([FromQuery] ZadaciGetByIDRequest request, CancellationToken cancellationToken)
        {
            var zadatak = await _applicationDbContext.Zadatak.Where(x => x.RadnikID == request.ID).Select(x => new ZadaciGetByIDResponseZadaci()
            {
                Id = x.Id,
                DatumDodavanja = x.DatumDodavanja,
                DatumZavrsetka = x.DatumZavrsetka,
                Naziv = x.Naziv,
                Opis = x.Opis,
            }).ToListAsync(cancellationToken);

            return new ZadaciGetByIDResponse
            {
                Zadaci = zadatak
            };
        }
    }
}
