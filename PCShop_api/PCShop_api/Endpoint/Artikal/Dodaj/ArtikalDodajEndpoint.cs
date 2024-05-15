using Microsoft.AspNetCore.Mvc;
using PCShop_api.Data;
using PCShop_api.Data.Models;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Artikal.Dodaj
{
    [MyAuthorization]
    [Route("Artikal-Dodaj")]
    public class ArtikalDodajEndpoint : MyBaseEndpoint<ArtikalDodajRequest, ArtikalDodajResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ArtikalDodajEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<ArtikalDodajResponse> Akcija([FromBody] ArtikalDodajRequest request, CancellationToken cancellationToken)
        {
           

            var noviArt = new Data.Models.Artikal
            {
                ID = request.ID,
                ImeArtikla = request.ImeArtikla,
                Cijena = request.Cijena,
                Proizvodjac = request.Proizvodjac,
                TipID = request.TipID,
                //Slika = request.Slika,
                Opis = request.Opis
            };

            _applicationDbContext.Artikal.Add(noviArt);

            await _applicationDbContext.SaveChangesAsync();

            return new ArtikalDodajResponse
            {

            };
        }
    }
}
