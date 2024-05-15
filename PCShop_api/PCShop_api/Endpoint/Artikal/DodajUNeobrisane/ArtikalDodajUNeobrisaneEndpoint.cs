using Microsoft.AspNetCore.Mvc;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Artikal.DodajUNeobrisane
{
    [MyAuthorization]
    [Route("Artikal-DodajUNeobrisane")]
    public class ArtikalDodajUNeobrisaneEndpoint : MyBaseEndpoint<ArtikalDodajUNeobrisaneRequest, ArtikalDodajUNeobrisaneResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ArtikalDodajUNeobrisaneEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpPost]
        public override async Task<ArtikalDodajUNeobrisaneResponse> Akcija([FromBody] ArtikalDodajUNeobrisaneRequest request, CancellationToken cancellationToken)
        {
            var artikli = _applicationDbContext.Artikal.Where(x => x.ID == request.ArtID).FirstOrDefault();
            if (artikli == null)
            {
                throw new Exception("Nije pronadjen artikal za ID: " + request.ArtID);
            }
            artikli.ID = request.ArtID;
            artikli.isObrisan = false;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new ArtikalDodajUNeobrisaneResponse
            {
            };
        }

    }
}
