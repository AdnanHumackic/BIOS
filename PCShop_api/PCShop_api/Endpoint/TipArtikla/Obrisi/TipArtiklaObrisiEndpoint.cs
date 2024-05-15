using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Helper;

namespace PCShop_api.Endpoint.TipArtikla.Obrisi
{
    [Route("TipArtikla-Obrisi")]
    public class TipArtiklaObrisiEndpoint : MyBaseEndpoint<TipArtiklaObrisiRequest, TipArtiklaObrisiResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TipArtiklaObrisiEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpDelete]
        public override async Task<TipArtiklaObrisiResponse> Akcija([FromQuery] TipArtiklaObrisiRequest request, CancellationToken cancellationToken)
        {
            var tipArt = await _applicationDbContext.TipArtikla.Where(x => x.ID == request.ID).FirstOrDefaultAsync(cancellationToken);

            if (tipArt == null)
            {
                throw new Exception("Nije pronadjen artikal sa ID: " + request.ID);
            }

            _applicationDbContext.Remove(tipArt);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new TipArtiklaObrisiResponse
            {

            };
        }
    }
}
