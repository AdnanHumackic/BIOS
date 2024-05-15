using Microsoft.AspNetCore.Mvc;
using PCShop_api.Data;
using PCShop_api.Helper;

namespace PCShop_api.Endpoint.TipArtikla.Dodaj
{

    [Route("TipArtikla-Dodaj")]
    public class TipArtiklaDodajEndpoint : MyBaseEndpoint<TipArtiklaDodajRequest, TipArtiklaDodajResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TipArtiklaDodajEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpPost]
        public override async Task<TipArtiklaDodajResponse> Akcija([FromBody] TipArtiklaDodajRequest request, CancellationToken cancellationToken)
        {
            var noviTip = new Data.Models.TipArtikla
            {
                ID = request.ID,
                Tip = request.TipArtikla
            };
            _applicationDbContext.TipArtikla.Add(noviTip);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new TipArtiklaDodajResponse
            {

            };
        }
    }
}
