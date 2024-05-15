using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Helper;

namespace PCShop_api.Endpoint.Dostavljaci.GetAll
{
    [Route("Dostavljaci-GetAll")]
    public class DostavljacGetAllEndpoint:MyBaseEndpoint<DostavljacGetAllRequest,DostavljacGetAllResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DostavljacGetAllEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public override async Task<DostavljacGetAllResponse> Akcija([FromQuery]DostavljacGetAllRequest request, CancellationToken cancellationToken)
        {
            var dostavljacObj = await _applicationDbContext.Dostavljaci
                .Select(x => new DostavljacGetAllResponseDostavljac()
                {
                    ID = x.ID,
                    Naziv = x.Naziv,
                    CijenaDostave = x.CijenaDostave,
                    Sjediste = x.Sjediste
                }).ToListAsync(cancellationToken: cancellationToken);

            return new DostavljacGetAllResponse
            {
                Dostavljac = dostavljacObj
            };
        }
    }
}
