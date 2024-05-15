using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Kompatibilnost.GetAll
{
    [MyAuthorization]
    [Route("Kompatibilnost-GetAll")]
    public class KompatibilnostGetAllEndpoint : MyBaseEndpoint<KompatibilnostGetAllRequest, KompatibilnostGetAllResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public KompatibilnostGetAllEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public override async Task<KompatibilnostGetAllResponse> Akcija([FromQuery] KompatibilnostGetAllRequest request, CancellationToken cancellationToken)
        {
            var kompObj = await _applicationDbContext.Kompatibilnost
                .Select(x => new KompatibilnostGetAllResponseKompatibilnost()
                {
                    ID = x.ArtikalKompatibilnostID,
                    Artikal1 = x.Artikal1.ImeArtikla,
                    Artikal2 = x.Artikal2.ImeArtikla,
                    Art1ID = x.Artikal1ID,
                    Art2ID = x.Artikal2ID,

                }).ToListAsync(cancellationToken);

            return new KompatibilnostGetAllResponse
            {
                Kompatibilnost = kompObj
            };
        }
    }
}
