using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Artikal.GetZadnjeKreirani
{
    [MyAuthorization]
    [Route("Artikal-GetZadnjeKreirani")]
    public class ArtikalGetZadnjeKreiraniEndpoint : MyBaseEndpoint<ArtikalGetZadnjeKreiraniRequest, ArtikalGetZadnjeKreiraniResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ArtikalGetZadnjeKreiraniEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet]
        public override async Task<ArtikalGetZadnjeKreiraniResponse> Akcija([FromQuery] ArtikalGetZadnjeKreiraniRequest request, CancellationToken cancellationToken)
        {
            var zadnjeKreirani = await _applicationDbContext.Artikal.OrderByDescending(x => x.ID).FirstOrDefaultAsync(cancellationToken);

            return new ArtikalGetZadnjeKreiraniResponse
            {
                ID = zadnjeKreirani.ID,
                Ime = zadnjeKreirani.ImeArtikla
            };
        }
    }
}
