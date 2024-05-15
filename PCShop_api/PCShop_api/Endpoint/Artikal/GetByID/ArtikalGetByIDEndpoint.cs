using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Artikal.GetByID
{
    [Route("Artikal-GetByID")]
    public class ArtikalGetByIDEndpoint : MyBaseEndpoint<ArtikalGetByIDRequest, ArtikalGetByIDResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ArtikalGetByIDEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet]
        public override async Task<ArtikalGetByIDResponse> Akcija([FromQuery] ArtikalGetByIDRequest request, CancellationToken cancellationToken)
        {
            var artikal = await _applicationDbContext.Artikal.Where(x => x.ID == request.ID).FirstOrDefaultAsync(cancellationToken:cancellationToken);

            if (artikal == null)
            {
                throw new Exception("Unesite validan ID!");
            }

            return new ArtikalGetByIDResponse()
            {
                ID = artikal.ID,
                Naziv = artikal.ImeArtikla,
                Cijena = artikal.Cijena,
                Proizvodjac = artikal.Proizvodjac,
                //Slika = artikal.Slika,
                Opis = artikal.Opis,
                TipID=artikal.TipID
            };
        }
    }
}
