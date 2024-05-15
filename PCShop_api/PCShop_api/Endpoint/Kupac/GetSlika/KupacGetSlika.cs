using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace PCShop_api.Endpoint.Kupac.GetSlika
{
    [ApiController]
    [Route("Kupac")]
    public class KupacGetSlika:ControllerBase
    {
        [HttpGet("Slika")]
        public async Task<FileContentResult> GetByID(int id, CancellationToken cancellationToken)
        {
            var folderPath = "slike-korisnika";

            byte[] slika;
            try
            {
                var fileName = $"{folderPath}/{id}-velika.jpg";
                slika = await System.IO.File.ReadAllBytesAsync(fileName, cancellationToken);
                return File(slika, GetMimeType(fileName));
            }
            catch (Exception ex)
            {
                var fileName = $"wwwroot/profile_images/empty.png";
                slika = await System.IO.File.ReadAllBytesAsync(fileName, cancellationToken);
                return File(slika, GetMimeType(fileName));
            }

        }

        static string GetMimeType(string fileName)
        {
            // Create a new instance of FileExtensionContentTypeProvider
            var provider = new FileExtensionContentTypeProvider();

            // Try to get the MIME type
            if (provider.TryGetContentType(fileName, out var contentType))
            {
                return contentType;
            }

            // If the MIME type cannot be determined, you can provide a default or handle it accordingly
            return "application/octet-stream";
        }
    }
}

