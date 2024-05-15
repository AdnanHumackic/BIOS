using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Endpoint.Kupac.GetByID;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Kupac.LozinkaGet
{
    [MyAuthorization]
    [Route("Kupac-LozinkaGet")]
    public class KupacLozinkaGetEndpoint:MyBaseEndpoint<KupacLozinkaGetRequest, KupacLozinkaGetResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        
        public KupacLozinkaGetEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public override async Task<KupacLozinkaGetResponse> Akcija([FromQuery]KupacLozinkaGetRequest request, CancellationToken cancellationToken)
        {
            var lozinka = await _applicationDbContext.KorisnickiNalog.Where(x => x.ID == request.ID
           /*&& _authService.IsKupac()*/).FirstOrDefaultAsync(cancellationToken);

            return new KupacLozinkaGetResponse
            {
                ID = lozinka.ID,
                Lozinka = lozinka.Lozinka,
            };
        }
    }
}
