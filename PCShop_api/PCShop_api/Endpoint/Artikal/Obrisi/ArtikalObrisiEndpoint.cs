using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Artikal.Obrisi
{
    [MyAuthorization]
    [Route("Artikal-Obrisi")]
    public class ArtikalObrisiEndpoint : MyBaseEndpoint<ArtikalObrisiRequest, ArtikalObrisiResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ArtikalObrisiEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpDelete]
        public override async Task<ArtikalObrisiResponse> Akcija([FromQuery] ArtikalObrisiRequest request, CancellationToken cancellationToken)
        {
            var artikli = _applicationDbContext.Artikal.Where(x => x.ID == request.ID).FirstOrDefaultAsync(cancellationToken);

            if (artikli == null)
            {
                throw new Exception("Nije pronadjen artikal za ID: " + request.ID);
            }

            _applicationDbContext.Remove(artikli);
            await _applicationDbContext.SaveChangesAsync(cancellationToken:cancellationToken);

            return new ArtikalObrisiResponse
            {

            };
        }
    }
}
