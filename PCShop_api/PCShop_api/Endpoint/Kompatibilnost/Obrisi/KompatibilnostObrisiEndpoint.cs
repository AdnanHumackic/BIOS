using Microsoft.AspNetCore.Mvc;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Kompatibilnost.Obrisi
{
    [MyAuthorization]
    [Route("Kompatibilnost-Obrisi")]
    public class KompatibilnostObrisiEndpoint : MyBaseEndpoint<KompatibilnostObrisiRequest, KompatibilnostObrisiResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public KompatibilnostObrisiEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpDelete]
        public override async Task<KompatibilnostObrisiResponse> Akcija([FromQuery] KompatibilnostObrisiRequest request, CancellationToken cancellationToken)
        {
            var kompatibilnost = _applicationDbContext.Kompatibilnost.Where(x => x.ArtikalKompatibilnostID == request.ID).FirstOrDefault();

            if (kompatibilnost == null)
            {
                throw new Exception("Nije pronadjen ID za kompatibilnost: " + request.ID);
            }

            _applicationDbContext.Remove(kompatibilnost);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new KompatibilnostObrisiResponse
            {

            };
        }
    }
}
