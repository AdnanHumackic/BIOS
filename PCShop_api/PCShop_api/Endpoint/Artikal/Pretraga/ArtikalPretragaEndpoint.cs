using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Helper;

namespace PCShop_api.Endpoint.Artikal.Pretraga
{
    [Route("Artikal-Pretraga")]
    public class ArtikalPretragaEndpoint : MyBaseEndpoint<ArtikalPretragaRequest, ArtikalPretragaResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ArtikalPretragaEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet]
        public override async Task<ArtikalPretragaResponse> Akcija([FromQuery] ArtikalPretragaRequest request, CancellationToken cancellationToken)
        {
            var artikal = await _applicationDbContext.Artikal
                .Where(x =>
                    ((request.Pretraga == null && request.TipID == null) && x.isObrisan == false)
                || ((request.Pretraga != null && x.ImeArtikla.ToLower().StartsWith(request.Pretraga.ToLower()) && request.TipID != null && request.TipID == x.TipID) && x.isObrisan == false)
                || ((request.Pretraga == null && request.TipID != null && request.TipID == x.TipID) && x.isObrisan == false)
                || ((request.Pretraga != null && x.ImeArtikla.ToLower().StartsWith(request.Pretraga.ToLower()) && request.TipID == null) && x.isObrisan == false)
               )
                .Select(x => new ArtikalPretragaResponseArtikal
                {
                    ID = x.ID,
                    Naziv = x.ImeArtikla,
                    Cijena = x.Cijena,
                    Tip = x.TipArtikla.Tip,
                    Proizvodjac = x.Proizvodjac,
                    //Slika = x.Slika,
                    Opis = x.Opis
                }).ToListAsync(cancellationToken:cancellationToken);

            return new ArtikalPretragaResponse
            {
                Artikli = artikal
            };
        }
    }
}
