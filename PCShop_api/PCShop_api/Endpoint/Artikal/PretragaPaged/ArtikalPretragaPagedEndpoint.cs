using Microsoft.AspNetCore.Mvc;
using PCShop_api.Data;
using PCShop_api.Endpoint.Artikal.GetAllKojiSuObrisaniPaged;
using PCShop_api.Endpoint.Artikal.Pretraga;
using PCShop_api.Helper;

namespace PCShop_api.Endpoint.Artikal.PretragaPaged
{
    [Route("Artikal-PretragaPaged")]
    public class ArtikalPretragaPagedEndpoint:MyBaseEndpoint<ArtikalPretragaPagedRequest, ArtikalPretragaPagedResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ArtikalPretragaPagedEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public override async Task<ArtikalPretragaPagedResponse> Akcija([FromQuery]ArtikalPretragaPagedRequest request, CancellationToken cancellationToken)
        {
            var artikal = _applicationDbContext.Artikal
                .Where(x =>
                 (
                    request.Pretraga == null || x.ImeArtikla.ToLower().StartsWith(request.Pretraga.ToLower()))
                    && (request.TipID == null || x.TipID == request.TipID)
                    && (request.cijenaOd == null || x.Cijena >= request.cijenaOd)
                    && (request.cijenaDo == null || x.Cijena <= request.cijenaDo)
                    && (request.Proizvodjac==null || x.Proizvodjac.ToLower().StartsWith(request.Proizvodjac.ToLower()))
                    && x.isObrisan == false
                )
                .Select(x => new ArtikalPretragaPagedResponseArtikal
                {
                    ID = x.ID,
                    Naziv = x.ImeArtikla,
                    Cijena = x.Cijena,
                    Tip = x.TipArtikla.Tip,
                    Proizvodjac = x.Proizvodjac,
                    Opis = x.Opis
                });

            var dataOfOnePage = PagedList<ArtikalPretragaPagedResponseArtikal>.Create(artikal, request.PageNumber, request.PageSize);


            return new ArtikalPretragaPagedResponse
            {
                Artikal = dataOfOnePage
            }; ;
        }
    }
}
