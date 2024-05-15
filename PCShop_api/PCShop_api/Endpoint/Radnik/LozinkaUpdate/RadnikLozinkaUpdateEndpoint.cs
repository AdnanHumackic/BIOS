using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Data.Models;
using PCShop_api.Endpoint.Kupac.LozinkaUpdate;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;
using SkiaSharp;

namespace PCShop_api.Endpoint.Radnik.LozinkaUpdate
{
    [MyAuthorization]
    [Route("Radnik-LozinkaUpdate")]
    public class RadnikLozinkaUpdateEndpoint:MyBaseEndpoint<RadnikLozinkaUpdateRequest, int>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RadnikLozinkaUpdateEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<int> Akcija([FromBody]RadnikLozinkaUpdateRequest request, CancellationToken cancellationToken)
        {

            Data.Models.Radnik? radnik;
            if (request.ID == 0)
            {
                radnik = new Data.Models.Radnik();
                _applicationDbContext.Add(radnik);
                radnik.SlikaKorisnika = null;
            }
            else
            {
                radnik=_applicationDbContext.Radnik.Where(x=>x.ID==request.ID).FirstOrDefault();

                if (radnik == null)
                {
                    throw new Exception("Pogesan ID");
                }
            }


            radnik.Lozinka = request.Lozinka;
            if (!string.IsNullOrEmpty(request.SlikaRadnika))
            {
                byte[]? slika_bajtovi = request.SlikaRadnika?.ParsirajBase64();

                if (slika_bajtovi == null)
                    throw new Exception("Pogresan base64 format");

                byte[]? slika_bajtovi_resized_velika = resize(slika_bajtovi, 200);
                if (slika_bajtovi_resized_velika == null)
                    throw new Exception("Pogresan format slike");

                byte[]? slika_bajtovi_resized_mala = resize(slika_bajtovi, 50);
                if (slika_bajtovi_resized_mala == null)
                    throw new Exception("Pogresan format slike");

                var folderPath = "slike-korisnika";
                if (!Directory.Exists(folderPath))
                {
                    // Create the folder if it does not exist
                    Directory.CreateDirectory(folderPath);
                }

                await System.IO.File.WriteAllBytesAsync($"{folderPath}/{request.ID}-velika.jpg", slika_bajtovi_resized_velika, cancellationToken);
                await System.IO.File.WriteAllBytesAsync($"{folderPath}/{request.ID}-mala.jpg", slika_bajtovi_resized_mala, cancellationToken);

                //1- file system od web servera ili neki treci servis kao sto je azure blob store ili aws 
            }

            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return radnik.ID;

            // var lozinka = await _applicationDbContext.KorisnickiNalog.Where(x => x.ID == request.ID
            ///*&& _authService.IsRadnik()*/).FirstOrDefaultAsync(cancellationToken);

            // lozinka.Lozinka = request.Lozinka;

            // await _applicationDbContext.SaveChangesAsync(cancellationToken);

            // return new RadnikLozinkaUpdateResponse
            // {
            // };
        }
        public static byte[]? resize(byte[] slikaBajtovi, int size, int quality = 75)
        {
            using var input = new MemoryStream(slikaBajtovi);
            using var inputStream = new SKManagedStream(input);
            using var original = SKBitmap.Decode(inputStream);
            int width, height;
            if (original.Width > original.Height)
            {
                width = size;
                height = original.Height * size / original.Width;
            }
            else
            {
                width = original.Width * size / original.Height;
                height = size;
            }

            using var resized = original
                .Resize(new SKImageInfo(width, height), SKBitmapResizeMethod.Lanczos3);
            if (resized == null) return null;

            using var image = SKImage.FromBitmap(resized);
            return image.Encode(SKEncodedImageFormat.Jpeg, quality)
                .ToArray();
        }
    }
}
