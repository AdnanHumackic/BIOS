using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Zadaci.GetAll
{
    [MyAuthorization]
    [Route("Zadaci-GetAll")]
    public class ZadaciGetAllEndpoint:MyBaseEndpoint<ZadaciGetAllRequest,ZadaciGetAllResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ZadaciGetAllEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet]
        public override async Task<ZadaciGetAllResponse> Akcija([FromQuery] ZadaciGetAllRequest request, CancellationToken cancellationToken)
        {
            var zadaciLista = await _applicationDbContext.Zadatak.Select(x => new ZadaciGetAllResponseZadaci()
            {
                Id = x.Id,
                DatumDodavanja = x.DatumDodavanja,
                DatumZavrsetka = x.DatumZavrsetka,
                Naziv = x.Naziv,
                Opis = x.Opis
            }).ToListAsync(cancellationToken);

            return new ZadaciGetAllResponse
            {
                StavkeZadatak = zadaciLista
            };
        }
    }
}
