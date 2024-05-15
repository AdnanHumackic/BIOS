using Microsoft.AspNetCore.Mvc;
using PCShop_api.Data;
using PCShop_api.Data.Models;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;
using SkiaSharp;

namespace PCShop_api.Endpoint.ArtikalSlika.Update
{
    [MyAuthorization]
    [Route("ArtikalSlika-Update")]
    public class ArtikalSlikaUpdateEndpoint:MyBaseEndpoint<ArtikalSlikaUpdateRequest, ArtikalSlikaUpdateResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ArtikalSlikaUpdateEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<ArtikalSlikaUpdateResponse> Akcija([FromBody]ArtikalSlikaUpdateRequest request, CancellationToken cancellationToken)
        {
            var noveSlike = new List<Data.Models.ArtikalSlika>();
            if (request.SlikaArtikla != null)
            {
                for (int i = 0; i < request.SlikaArtikla.Count; i++)
            {
                var slikaObj = new Data.Models.ArtikalSlika
                {
                    ArtikalID = request.IDArtikla
                };

                if (!string.IsNullOrEmpty(request.SlikaArtikla[i]))
                {
                    byte[]? slika_bajtovi = request.SlikaArtikla[i]?.ParsirajBase64();

                    if (slika_bajtovi == null)
                        throw new Exception("Pogresan base64 format");
                    //og200
                    byte[]? slika_bajtovi_resized_velika = resize(slika_bajtovi, 500);
                    if (slika_bajtovi_resized_velika == null)
                        throw new Exception("Pogresan format slike");

                    byte[]? slika_bajtovi_resized_mala = resize(slika_bajtovi, 50);
                    if (slika_bajtovi_resized_mala == null)
                        throw new Exception("Pogresan format slike");

                    var folderPath = "slike-artikla";
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    var velikaFileName = $"{request.IDArtikla}-{i + 1}-velika.jpg";
                    var malaFileName = $"{request.IDArtikla}-{i + 1}-mala.jpg";

                    await System.IO.File.WriteAllBytesAsync(Path.Combine(folderPath, velikaFileName), slika_bajtovi_resized_velika, cancellationToken);
                    await System.IO.File.WriteAllBytesAsync(Path.Combine(folderPath, malaFileName), slika_bajtovi_resized_mala, cancellationToken);

                }
            }
        }

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new ArtikalSlikaUpdateResponse
            {
                
            };

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
