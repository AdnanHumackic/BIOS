using Microsoft.AspNetCore.Mvc;
using PCShop_api.Data;
using PCShop_api.Endpoint.Artikal.Update;
using PCShop_api.Helper;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Kupac.Update
{
    [MyAuthorization]
    [Route("Kupac-Update")]
    public class KupacUpdateEndpoint:MyBaseEndpoint<KupacUpdateRequest, int>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        
        public KupacUpdateEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<int> Akcija([FromBody]KupacUpdateRequest request, CancellationToken cancellationToken)
        {
            Data.Models.Kupac? kupac;
            if (request.ID == 0)
            {
                kupac = new Data.Models.Kupac();
                _applicationDbContext.Add(kupac);
                kupac.SlikaKorisnika = null;
            }
            else
            {
                kupac = _applicationDbContext.Kupac.Where(x => x.ID == request.ID).FirstOrDefault();

                if (kupac == null)
                    throw new Exception("Pogresan ID");
            }


            //if (kupac == null)
            //{
            //    throw new Exception("Nije pronadjen kupac za ID: " + request.ID);
            //}

            kupac.KorisnickoIme= request.KorisnickoIme;
            kupac.Kupac.Ime = request.Ime;
            kupac.Kupac.Prezime = request.Prezime;
            kupac.Kupac.DrzavaID = request.Drzava;
            kupac.Kupac.Email = request.Email;

            if (!string.IsNullOrEmpty(request.SlikaKorisnika))
            {
                byte[]? slika_bajtovi = request.SlikaKorisnika?.ParsirajBase64();

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

            //return new KupacUpdateResponse
            //{
            //};
            return kupac.ID;
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
