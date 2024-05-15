using Microsoft.AspNetCore.Mvc;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;
using SkiaSharp;

namespace PCShop_api.Endpoint.Admin.Update
{
    [MyAuthorization]
    [Route("Admin-Update")]
    public class AdminUpdatePodaciEndpoint:MyBaseEndpoint<AdminUpdatePodaciRequest,int>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AdminUpdatePodaciEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpPost]
        public override async Task<int> Akcija([FromBody]AdminUpdatePodaciRequest request, CancellationToken cancellationToken)
        {
            Data.Models.Admin? admin;
            if (request.ID == 0)
            {
                admin = new Data.Models.Admin();
                _applicationDbContext.Add(admin);
                admin.SlikaKorisnika = null;
            }
            else
            {
                admin = _applicationDbContext.Admin.Where(x=>x.ID == request.ID).FirstOrDefault();

                if(admin == null)
                {
                    throw new Exception("Pogresan ID");
                }
            }

            admin.KorisnickoIme=request.KorisnickoIme;
            admin.Admin.Ime = request.Ime;
            admin.Admin.Prezime = request.Prezime;
            admin.Admin.DrzavaID = request.Drzava;

            if (!string.IsNullOrEmpty(request.SlikaKorisnika))
            {
                byte[]? slika_bajtovi = request.SlikaKorisnika?.ParsirajBase64();

                if (slika_bajtovi == null)
                    throw new Exception("Pogresan base64 format");

                byte[]? slika_bajtovi_resized_velika = resize(slika_bajtovi, 200);
                if(slika_bajtovi_resized_velika == null)
                    throw new Exception("Pogresan format slike");

                byte[]? slika_bajtovi_resized_mala = resize(slika_bajtovi, 50);
                if (slika_bajtovi_resized_mala == null)
                    throw new Exception("Pogresan format slike");

                var folderPath = "slike-korisnika";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                await System.IO.File.WriteAllBytesAsync($"{folderPath}/{request.ID}-velika.jpg", slika_bajtovi_resized_velika, cancellationToken);
                await System.IO.File.WriteAllBytesAsync($"{folderPath}/{request.ID}-mala.jpg", slika_bajtovi_resized_mala, cancellationToken);

            }

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return admin.ID;
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
