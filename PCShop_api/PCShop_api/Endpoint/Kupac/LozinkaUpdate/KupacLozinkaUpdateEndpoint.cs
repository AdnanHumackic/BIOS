using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Endpoint.Kupac.GetByID;
using PCShop_api.Endpoint.Kupac.Update;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Kupac.LozinkaUpdate
{
    [MyAuthorization]
    [Route("Kupac-LozinkaUpdate")]
    public class KupacLozinkaUpdateEndpoint:MyBaseEndpoint<KupacLozinkaUpdateRequest, KupacLozinkaUpdateResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public KupacLozinkaUpdateEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<KupacLozinkaUpdateResponse> Akcija([FromBody]KupacLozinkaUpdateRequest request, CancellationToken cancellationToken)
        {
            var lozinka = await _applicationDbContext.KorisnickiNalog.Where(x => x.ID == request.ID
            /*&& _authService.IsKupac()*/).FirstOrDefaultAsync(cancellationToken);

            lozinka.Lozinka = request.Lozinka;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new KupacLozinkaUpdateResponse
            {
            };
        }
    }
}
