using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Endpoint.KorisnickiNalog.UpdateZaAdmina;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.KorisnickiNalog.Update
{
    [MyAuthorization]
    [Route("Korisnik-UlogaUpdate")]
    public class UlogaUpdateEndpoint:MyBaseEndpoint<AdminUpdateRequest,int>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UlogaUpdateEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<int> Akcija([FromBody]AdminUpdateRequest request, CancellationToken cancellationToken)
        {
            Data.Models.KorisnickiNalog? korisnik;

            if(request.ID== 0)
            {
                korisnik=new Data.Models.KorisnickiNalog();
                _applicationDbContext.Add(korisnik);
                korisnik.SlikaKorisnika = null;
            }
            else
            {
                korisnik=_applicationDbContext.KorisnickiNalog.Where(x=>x.ID == request.ID).FirstOrDefault();   

                if(korisnik==null) {
                    throw new Exception("Pogresan ID");
                }
            }
            korisnik.ID = request.ID;
            

            await _applicationDbContext.SaveChangesAsync();

            return korisnik.ID;
        }
    }
}
