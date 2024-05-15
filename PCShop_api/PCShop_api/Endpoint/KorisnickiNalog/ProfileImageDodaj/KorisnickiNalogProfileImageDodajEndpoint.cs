using Microsoft.AspNetCore.Mvc;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.KorisnickiNalog.ProfileImageDodaj
{
    [MyAuthorization]
    [Route("KorisnickiNalog-ProfileImageDodaj")]
    public class KorisnickiNalogProfileImageDodajEndpoint:MyBaseEndpoint<KorisnickiNalogProfileImageDodajRequest, int>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public KorisnickiNalogProfileImageDodajEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<int> Akcija([FromBody]KorisnickiNalogProfileImageDodajRequest request, CancellationToken cancellationToken)
        {
            Data.Models.KorisnickiNalog? korisnickiNalog = _applicationDbContext.KorisnickiNalog.Where(x => x.ID == request.IDKorisnika).FirstOrDefault();

            if (korisnickiNalog == null)
                throw new Exception("Neispravan ID");
            if (request.SlikaKorisnika.Length > 300 * 1000)
                throw new Exception("Maksimalna velicina fajla je 300KB!");

            string ekstenzija = Path.GetExtension(request.SlikaKorisnika.FileName);

            var filename = $"{Guid.NewGuid()}{ekstenzija}";

            await request.SlikaKorisnika.CopyToAsync(new FileStream(Config.SlikeFolder + filename, FileMode.Create), cancellationToken);



            //kupac.SlikaKorisnika = filename + Config.Slike;
            await _applicationDbContext.SaveChangesAsync(cancellationToken);


            return korisnickiNalog.ID;
        }
    }
}
