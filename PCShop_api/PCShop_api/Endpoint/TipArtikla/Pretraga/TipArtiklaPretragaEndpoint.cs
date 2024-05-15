using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Helper;

namespace PCShop_api.Endpoint.TipArtikla.Pretraga
{
    [Route("TipArtikla-Pretraga")]
    public class TipArtiklaPretragaEndpoint : MyBaseEndpoint<TipArtiklaPretragaRequest, TipArtiklaPretragaResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TipArtiklaPretragaEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet]
        public override async Task<TipArtiklaPretragaResponse> Akcija([FromQuery] TipArtiklaPretragaRequest request, CancellationToken cancellationToken)
        {
            var tipObj = await _applicationDbContext
                .TipArtikla
                .Where(x => request.Naziv == null || x.Tip.ToLower().StartsWith(request.Naziv.ToLower()))
                .Select(x => new TipArtiklaPretragaResponseTipArtikla()
                {
                    ID = x.ID,
                    Tip = x.Tip

                }).ToListAsync(cancellationToken);

            return new TipArtiklaPretragaResponse
            {
                TipArtikala = tipObj
            };
        }
    }
}
