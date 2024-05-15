using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Admin.GetByID
{
    [MyAuthorization]
    [Route("Admin-GetByID")]
    public class AdminGetByIDEndpoint:MyBaseEndpoint<AdminGetByIDRequest,AdminGetByIDResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _authService;

        public AdminGetByIDEndpoint(ApplicationDbContext applicationDbContext, MyAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
        }

        [HttpGet]
        public override async Task<AdminGetByIDResponse> Akcija([FromQuery]AdminGetByIDRequest request, CancellationToken cancellationToken)
        {
            var admin = await _applicationDbContext.KorisnickiNalog.Where(x => x.ID == request.ID).FirstOrDefaultAsync(cancellationToken);

            return new AdminGetByIDResponse
            {
                ID = admin.ID,
                KorisnickoIme = admin.KorisnickoIme,
                Lozinka = admin.Lozinka,
                Ime = admin.Admin.Ime,
                Prezime = admin.Admin.Prezime,
                Drzava = admin.Admin.DrzavaID,
                SlikaKorisnika = admin.SlikaKorisnika
            };
        }
    }
}
