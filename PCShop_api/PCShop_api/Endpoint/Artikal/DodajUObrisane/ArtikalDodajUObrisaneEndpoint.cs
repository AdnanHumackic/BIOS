using Microsoft.AspNetCore.Mvc;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Artikal.DodajUObrisane
{
    [MyAuthorization]
    [Route("Artikal-DodajUObrisane")]

    public class ArtikalDodajUObrisaneEndpoint : MyBaseEndpoint<ArtikalDodajUObrisaneRequest, ArtikalDodajUObrisaneResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ArtikalDodajUObrisaneEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<ArtikalDodajUObrisaneResponse> Akcija([FromBody] ArtikalDodajUObrisaneRequest request, CancellationToken cancellationToken)
        {
            var artikli = _applicationDbContext.Artikal.Where(x => x.ID == request.ArtID).FirstOrDefault();
            if (artikli == null)
            {
                throw new Exception("Nije pronadjen artikal za ID: " + request.ArtID);
            }
            artikli.ID = request.ArtID;
            artikli.isObrisan = true;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new ArtikalDodajUObrisaneResponse
            {
            };
        }
    }
}
