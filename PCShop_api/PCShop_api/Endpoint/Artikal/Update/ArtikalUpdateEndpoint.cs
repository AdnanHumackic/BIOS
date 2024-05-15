using Microsoft.AspNetCore.Mvc;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Artikal.Update
{
    [MyAuthorization]
    [Route("Artikal-Update")]
    public class ArtikalUpdateEndpoint : MyBaseEndpoint<ArtikalUpdateRequest, ArtikalUpdateResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ArtikalUpdateEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<ArtikalUpdateResponse> Akcija([FromBody] ArtikalUpdateRequest request, CancellationToken cancellationToken)
        {

            var artikli = _applicationDbContext.Artikal.Where(x => x.ID == request.ID).FirstOrDefault();
            if (artikli == null)
            {
                throw new Exception("Nije pronadjen artikal za ID: " + request.ID);
            }

            artikli.ImeArtikla = request.ImeArtikla;
            artikli.Proizvodjac = request.Proizvodjac;
            artikli.Cijena = request.Cijena;
            //artikli.Slika = request.Slika;
            artikli.Opis = request.Opis;
            artikli.TipID = request.TipID;
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new ArtikalUpdateResponse
            {
            };

        }
    }
}
