using Microsoft.AspNetCore.Mvc;
using PCShop_api.Data;
using PCShop_api.Endpoint.Artikal.GetAllKojiNisuObrisaniPaged;
using PCShop_api.Endpoint.Artikal.GetAllKojiSuObrisani;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Artikal.GetAllKojiSuObrisaniPaged
{
    [MyAuthorization]
    [Route("Artikal-GetAllKojiSuObrisaniPaged")]
    public class ArtikalGetAllKojiSuObrisaniPagedEndpoint:MyBaseEndpoint<ArtikalGetAllKojiSuObrisaniPagedRequest, ArtikalGetAllKojiSuObrisaniPagedResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ArtikalGetAllKojiSuObrisaniPagedEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public override async Task<ArtikalGetAllKojiSuObrisaniPagedResponse> Akcija([FromQuery]ArtikalGetAllKojiSuObrisaniPagedRequest request, CancellationToken cancellationToken)
        {
            var art = _applicationDbContext.Artikal.Where(x => x.isObrisan == true)
               .Select(x => new ArtikalGetAllKojiSuObrisaniPagedResponseArtikal()
               {
                   ID = x.ID,
                   Naziv = x.ImeArtikla,
                   Cijena = x.Cijena,
                   Proizvodjac = x.Proizvodjac,
                   Tip = x.TipArtikla.Tip,
                   Opis = x.Opis,
                   isObrisan = x.isObrisan,
               });

            var dataOfOnePage = PagedList<ArtikalGetAllKojiSuObrisaniPagedResponseArtikal>.Create(art, request.PageNumber, request.PageSize);

            return new ArtikalGetAllKojiSuObrisaniPagedResponse
            {
                Artikli = dataOfOnePage
            };
        }
    }
}
