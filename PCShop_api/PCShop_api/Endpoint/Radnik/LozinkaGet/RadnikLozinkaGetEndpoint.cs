using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Endpoint.Kupac.LozinkaGet;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Radnik.LozinkaGet
{
    [MyAuthorization]
    [Route("Radnik-LozinkaGet")]
    public class RadnikLozinkaGetEndpoint:MyBaseEndpoint<RadnikLozinkaGetRequest, RadnikLozinkaGetResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RadnikLozinkaGetEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public override async Task<RadnikLozinkaGetResponse> Akcija([FromQuery]RadnikLozinkaGetRequest request, CancellationToken cancellationToken)
        {
            var lozinka = await _applicationDbContext.KorisnickiNalog.Where(x => x.ID == request.ID
          /*&& _authService.IsRadnik()*/).FirstOrDefaultAsync(cancellationToken);

            return new RadnikLozinkaGetResponse
            {
                ID = lozinka.ID,
                Lozinka = lozinka.Lozinka,
                SlikaRadnika=lozinka.SlikaKorisnika
            };
        }
    }
}
