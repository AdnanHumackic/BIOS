using Microsoft.AspNetCore.Mvc;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Kupac.ProfileImageDodaj
{
    [MyAuthorization]
    [Route("Kupac-ProfileImageDodaj")]
    public class KupacProfileImageDodajEndpoint:MyBaseEndpoint<KupacProfileImageDodajRequest, int>
    {

        private readonly ApplicationDbContext _applicationDbContext;

        public KupacProfileImageDodajEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<int> Akcija([FromBody]KupacProfileImageDodajRequest request, CancellationToken cancellationToken)
        {
            Data.Models.Kupac? kupac = _applicationDbContext.Kupac.Where(x => x.ID == request.KupacID).FirstOrDefault();

            if (kupac == null)
                throw new Exception("Neispravan ID");
            if (request.SlikaKupca.Length > 300 * 1000)
                throw new Exception("Maksimalna velicina fajla je 300KB!");

            string ekstenzija = Path.GetExtension(request.SlikaKupca.FileName);

            var filename = $"{Guid.NewGuid()}{ekstenzija}";

            await request.SlikaKupca.CopyToAsync(new FileStream(Config.SlikeFolder + filename, FileMode.Create), cancellationToken);



            //kupac.SlikaKorisnika = filename + Config.Slike;
            await _applicationDbContext.SaveChangesAsync(cancellationToken);


            return kupac.ID;

        }
    }
}
