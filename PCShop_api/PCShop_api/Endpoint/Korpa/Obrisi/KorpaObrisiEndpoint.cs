
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Korpa.Obrisi
{
    [MyAuthorization]
    [Route("Korpa-Obrisi")]
    public class KorpaObrisiEndpoint : MyBaseEndpoint<KorpaObrisiRequest,KorpaObrisiResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public KorpaObrisiEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpDelete]
        public override async Task<KorpaObrisiResponse> Akcija([FromQuery] KorpaObrisiRequest request, CancellationToken cancellationToken)
        {
            var korpaItem = await _applicationDbContext.Korpa.Where(x => x.ArtikalID == request.ID).FirstOrDefaultAsync(cancellationToken);

            if(korpaItem == null)
            {
                throw new Exception("Nije pronadjen Id za artikal: " + request.ID);
            }

            _applicationDbContext.Korpa.Remove(korpaItem);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new KorpaObrisiResponse();
        }

    }
}
