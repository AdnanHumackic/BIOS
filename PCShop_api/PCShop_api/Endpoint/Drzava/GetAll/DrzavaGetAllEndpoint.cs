using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Endpoint.Artikal.GetAll;
using PCShop_api.Helper;

namespace PCShop_api.Endpoint.Drzava.GetAll
{
    [Route("Drzava-GetAll")]
    public class DrzavaGetAllEndpoint:MyBaseEndpoint<DrzavaGetAllRequest, DrzavaGetAllResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DrzavaGetAllEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public override async Task<DrzavaGetAllResponse> Akcija([FromQuery]DrzavaGetAllRequest request, CancellationToken cancellationToken)
        {
            var drzavaObj = await _applicationDbContext.Drzava
               .Select(x => new DrzavaGetAllResponseDrzava()
               {
                   ID = x.ID,
                   Naziv = x.Naziv,
                   Skracenica = x.Skracenica
               })
               .ToListAsync(cancellationToken: cancellationToken);

            return new DrzavaGetAllResponse
            {
                Drzava=drzavaObj
            };
        }
    }
}
