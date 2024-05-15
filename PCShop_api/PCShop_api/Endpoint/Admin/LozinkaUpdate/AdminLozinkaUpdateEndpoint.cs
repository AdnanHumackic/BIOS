using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Admin.LozinkaUpdate
{
    [MyAuthorization]
    [Route("Admin-LozinkaUpdate")]
    public class AdminLozinkaUpdateEndpoint:MyBaseEndpoint<AdminLozinkaUpdateRequest,AdminLozinkaUpdateResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AdminLozinkaUpdateEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<AdminLozinkaUpdateResponse> Akcija([FromBody]AdminLozinkaUpdateRequest request, CancellationToken cancellationToken)
        {
            var lozinka = await _applicationDbContext.KorisnickiNalog.Where(x => x.ID == request.ID).FirstOrDefaultAsync(cancellationToken);

            lozinka.Lozinka = request.Lozinka;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new AdminLozinkaUpdateResponse
            {

            };
        }
    }
}
