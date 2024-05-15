using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PCShop_api.Endpoint.ArtikalSlika.Preview
{
    [ApiController]
    [Route("ArtikalSlikaPreview")]
    public class ArtikalSlikaPreview : ControllerBase
    {
        [HttpGet("Slika")]
        public async Task<FileContentResult> GetByID(int id, CancellationToken cancellationToken)
        {

            var folderPath = "slike-artikla";

            byte[] slika;
            try
            {
                var fileNamePattern = $"{id}-*-velika.jpg";
                var slikaFileName = Directory.EnumerateFiles(folderPath, fileNamePattern).FirstOrDefault();

                if (slikaFileName == null)
                {
                    fileNamePattern = "empty.jpg";
                    slikaFileName = Path.Combine("wwwroot/artikal_image", fileNamePattern);
                }

                slika = await System.IO.File.ReadAllBytesAsync(slikaFileName, cancellationToken);
                return File(slika, GetMimeType(slikaFileName));
            }
            catch (Exception ex)
            {
                var fileName = "wwwroot/artikal_image/empty.jpg";
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
