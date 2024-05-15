using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Admin.LozinkaGet
{
    [MyAuthorization]
    [Route("Admin-LozinkaGet")]
    public class AdminLozinkaGetEndpoint:MyBaseEndpoint<AdminLozinkaGetRequest,AdminLozinkaGetResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AdminLozinkaGetEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public override async Task<AdminLozinkaGetResponse> Akcija([FromQuery]AdminLozinkaGetRequest request, CancellationToken cancellationToken)
        {
            var lozinka = await _applicationDbContext.KorisnickiNalog.Where(x => x.ID == request.ID).FirstOrDefaultAsync(cancellationToken);

            return new AdminLozinkaGetResponse
            {
                ID = lozinka.ID,
                Lozinka = lozinka.Lozinka
            };
        }
    }
}
