using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Artikal.GetAllOsimProslijedjeniID
{
    [MyAuthorization]
    [Route("Artikal-GetAllOsimProslijednjenog")]
    public class ArtikalGetAllOsimProslijedjeniEndpoint : MyBaseEndpoint<ArtikalGetAllOsimProslijedjeniRequest, ArtikalGetAllOsimProslijedjeniResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ArtikalGetAllOsimProslijedjeniEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public override async Task<ArtikalGetAllOsimProslijedjeniResponse> Akcija([FromQuery] ArtikalGetAllOsimProslijedjeniRequest request, CancellationToken cancellationToken)
        {
            var artikli = await _applicationDbContext.Artikal.Where(x => x.ID != request.ID)
                .Select(x => new ArtikalGetAllOsimProslijedjeniResponseArtikal
                {
                    ID = x.ID,
                    Naziv = x.ImeArtikla
                }).ToListAsync(cancellationToken:cancellationToken);

            return new ArtikalGetAllOsimProslijedjeniResponse
            {
                Artikal = artikli
            };
        }
    }
}
