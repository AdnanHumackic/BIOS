using Microsoft.AspNetCore.Mvc;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Korisnik.ObrisiProfil
{
    [MyAuthorization]
    [Route("Profil-Obrisi")]
    public class ProfilObrisiEndpoint:MyBaseEndpoint<ProfilObrisiRequest,ProfilObrisiResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ProfilObrisiEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpDelete]
        public override async Task<ProfilObrisiResponse> Akcija([FromQuery]ProfilObrisiRequest request, CancellationToken cancellationToken)
        {
            var korisnickiNalog = _applicationDbContext.KorisnickiNalog.Where(x=>x.ID == request.ID).FirstOrDefault();
            var authToken = _applicationDbContext.AutentifikacijaToken.Where(x => x.KorisnickiNalogID == request.ID).ToList();

            if (korisnickiNalog.isAdmin)
            {
                _applicationDbContext.Admin.Remove(korisnickiNalog.Admin);
            } 
            else if (korisnickiNalog.isKupac)
            {
                _applicationDbContext.Kupac.Remove(korisnickiNalog.Kupac);
            }
            else if(korisnickiNalog.isRadnik)
            {
                _applicationDbContext.Radnik.Remove(korisnickiNalog.Radnik);
            }

            foreach (var token in authToken)
            {
                _applicationDbContext.AutentifikacijaToken.Remove(token);
            }

            _applicationDbContext.Remove(korisnickiNalog);
            await _applicationDbContext.SaveChangesAsync();

            return new ProfilObrisiResponse
            {

            };
        }
    }
}
