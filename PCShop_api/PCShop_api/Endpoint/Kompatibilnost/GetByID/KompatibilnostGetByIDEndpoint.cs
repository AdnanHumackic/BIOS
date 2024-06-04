using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Helper;

namespace PCShop_api.Endpoint.Kompatibilnost.GetByID
{
    [Route("Kompatibilnost-GetByID")]
    public class KompatibilnostGetByIDEndpoint : MyBaseEndpoint<KompatibilnostGetByIDRequest, KompatibilnostGetByIDResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public KompatibilnostGetByIDEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public override async Task<KompatibilnostGetByIDResponse> Akcija([FromQuery] KompatibilnostGetByIDRequest request, CancellationToken cancellationToken)
        {
            var komp = await _applicationDbContext.Kompatibilnost.Where(x => x.Artikal1.ID == request.ID
            && x.Artikal1ID == request.ID).Select(x => new KompatibilnostGetByIDResponseKompatibilnost()
            {
                ID = x.Artikal2.ID,
                Artikal2Ime = x.Artikal2.ImeArtikla,
                KompatibilnostID = x.ArtikalKompatibilnostID,
                Cijena=x.Artikal2.Cijena
            }).ToListAsync(cancellationToken);

            return new KompatibilnostGetByIDResponse
            {
                Komp = komp
            };
        }
    }
}
