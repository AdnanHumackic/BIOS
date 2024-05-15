using Microsoft.AspNetCore.Mvc;
using PCShop_api.Data;
using PCShop_api.Endpoint.Zadaci.GetByID;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Zadaci.GetByIDPaged
{
    [MyAuthorization]
    [Route("Zadaci-GetByIDPaged")]
    public class ZadaciGetByIDPagedEndpoint:MyBaseEndpoint<ZadaciGetByIDPagedRequest,ZadaciGetByIDPagedResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ZadaciGetByIDPagedEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public override async Task<ZadaciGetByIDPagedResponse> Akcija([FromQuery]ZadaciGetByIDPagedRequest request, CancellationToken cancellationToken)
        {
            var zadatak = _applicationDbContext.Zadatak.Where(x => x.RadnikID == request.ID).Select(x => new ZadaciGetByIDPagedResponseZadaci()
            {
                Id = x.Id,
                DatumDodavanja = x.DatumDodavanja,
                DatumZavrsetka = x.DatumZavrsetka,
                Naziv = x.Naziv,
                Opis = x.Opis,
            });

            var dataOfOnePage = PagedList<ZadaciGetByIDPagedResponseZadaci>.Create(zadatak, request.PageNumber, request.PageSize);
            return new ZadaciGetByIDPagedResponse
            {
                Zadaci = dataOfOnePage
            };
        }
    }
}
