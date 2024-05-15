using Microsoft.AspNetCore.Mvc;
using PCShop_api.Data;
using PCShop_api.Endpoint.Kupac.ProfileImageDodaj;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Radnik.ProfileImageDodaj
{
    [MyAuthorization]
    [Route("Radnik-ProfileImageDodaj")]

    public class RadnikProfileImageDodajEndpoint:MyBaseEndpoint<RadnikProfileImageDodajRequest, int>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RadnikProfileImageDodajEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<int> Akcija([FromBody] RadnikProfileImageDodajRequest request, CancellationToken cancellationToken)
        {
            Data.Models.Radnik? radnik = _applicationDbContext.Radnik.Where(x => x.ID == request.RadnikID).FirstOrDefault();

            if (radnik == null)
                throw new Exception("Neispravan ID");
            if (request.SlikaRadka.Length > 300 * 1000)
                throw new Exception("Maksimalna velicina fajla je 300KB!");

            string ekstenzija = Path.GetExtension(request.SlikaRadka.FileName);

            var filename = $"{Guid.NewGuid()}{ekstenzija}";

            await request.SlikaRadka.CopyToAsync(new FileStream(Config.SlikeFolder + filename, FileMode.Create), cancellationToken);



            //kupac.SlikaKorisnika = filename + Config.Slike;
            await _applicationDbContext.SaveChangesAsync(cancellationToken);


            return radnik.ID;

        }
    }
}
