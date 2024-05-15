using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Endpoint.Artikal.GetAllKojiNisuObrisani;
using PCShop_api.Helper;

namespace PCShop_api.Endpoint.Artikal.GetAllKojiNisuObrisaniPaged
{
    [Route("Artikal-GetAllKojiNisuObrisaniPaged")]
    public class ArtikalGetAllKojiNisuObrisaniPagedEndpoint:MyBaseEndpoint<ArtikalGetAllKojiNisuObrisaniPagedRequest, ArtikalGetAllKojiNisuObrisaniPagedResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ArtikalGetAllKojiNisuObrisaniPagedEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public override async Task<ArtikalGetAllKojiNisuObrisaniPagedResponse> Akcija([FromQuery]ArtikalGetAllKojiNisuObrisaniPagedRequest request, CancellationToken cancellationToken)
        {
            var art = _applicationDbContext.Artikal.Where(x => x.isObrisan == false)
               .Select(x => new ArtikalGetAllKojiNisuObrisaniPagedResponseArtikal()
               {
                   ID = x.ID,
                   Naziv = x.ImeArtikla,
                   Cijena = x.Cijena,
                   Proizvodjac = x.Proizvodjac,
                   Tip = x.TipArtikla.Tip,
                   Opis = x.Opis,
                   isObrisan = x.isObrisan
               });

            var dataOfOnePage=PagedList<ArtikalGetAllKojiNisuObrisaniPagedResponseArtikal>.Create(art, request.PageNumber, request.PageSize);

            return new ArtikalGetAllKojiNisuObrisaniPagedResponse
            {
                Artikli = dataOfOnePage
            };
        }
    }
}
