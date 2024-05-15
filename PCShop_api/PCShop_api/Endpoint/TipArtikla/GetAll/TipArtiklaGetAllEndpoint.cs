using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Helper;

namespace PCShop_api.Endpoint.TipArtikla.GetAll
{
    [Route("TipArtikla-GetAll")]
    public class TipArtiklaGetAllEndpoint : MyBaseEndpoint<TipArtiklaGetAllRequest, TipArtiklaGetAllResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TipArtiklaGetAllEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public override async Task<TipArtiklaGetAllResponse> Akcija([FromQuery] TipArtiklaGetAllRequest request, CancellationToken cancellationToken)
        {
            var tipObj = await _applicationDbContext.TipArtikla
                .Select(x => new TipArtiklaGetAllResponseTip()
                {
                    ID = x.ID,
                    TipArtikla = x.Tip
                }).ToListAsync(cancellationToken);

            return new TipArtiklaGetAllResponse
            {
                Tip = tipObj
            };
        }
    }
}
