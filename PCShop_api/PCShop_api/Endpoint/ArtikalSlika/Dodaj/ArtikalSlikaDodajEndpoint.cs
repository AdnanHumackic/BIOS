using Microsoft.AspNetCore.Mvc;
using PCShop_api.Data;
using PCShop_api.Endpoint.Kompatibilnost.Dodaj;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.ArtikalSlika.Dodaj
{
    [MyAuthorization]
    [Route("ArtikalSlika-Dodaj")]
    public class ArtikalSlikaDodajEndpoint:MyBaseEndpoint<ArtikalSlikaDodajRequest, int>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        
        public ArtikalSlikaDodajEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<int> Akcija([FromBody]ArtikalSlikaDodajRequest request, CancellationToken cancellationToken)
        {
            Data.Models.ArtikalSlika? artikal= _applicationDbContext.ArtikalSlika.Where(x=>x.Artikal.ID==request.ArtikalID).FirstOrDefault();

            if (artikal == null)
                throw new Exception("Neispravan ID");
            if (request.SlikaArtikla.Length > 300 * 1000)
                throw new Exception("Maksimalna velicina fajla je 300KB!");

            string ekstenzija = Path.GetExtension(request.SlikaArtikla.FileName);

            var filename = $"{Guid.NewGuid()}{ekstenzija}";

            await request.SlikaArtikla.CopyToAsync(new FileStream(Config.SlikeFolder + filename, FileMode.Create), cancellationToken);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return artikal.ID;
        }
    }
}
