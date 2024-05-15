using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using PCShop_api.Data.Models;

namespace PCShop_api.Endpoint.ArtikalSlika.Get
{        

    [ApiController]
    [Route("ArtikalSlika")]
    public class ArtikalSlikaGetSlika : ControllerBase
    {
        public class SlikaResponse
        {
            public byte[] Slika { get; set; }
            public int RedniBroj { get; set; }
        }

        [HttpGet("ArtikalSlika")]
        public async Task<List<SlikaResponse>> GetByID(int id, CancellationToken cancellationToken)
        {
            var folderPath = "slike-artikla";
            var slike = new List<SlikaResponse>();

            try
            {
                var sveSlike = Directory.GetFiles(folderPath, $"{id}-*-velika.jpg");

                foreach (var slikaFileName in sveSlike)
                {
                    var redniBroj = ExtractRedniBrojFromFileName(slikaFileName);

                    var slikaBytes = await System.IO.File.ReadAllBytesAsync(slikaFileName, cancellationToken);
                    slike.Add(new SlikaResponse { RedniBroj = redniBroj, Slika = slikaBytes });
                }

                return slike;
            }
            catch (Exception ex)
            {
                return new List<SlikaResponse>();
            }
        }
        private int ExtractRedniBrojFromFileName(string fileName)
        {

            var parts = fileName.Split('-');
            if (parts.Length >= 2 && int.TryParse(parts[parts.Length - 2], out var redniBroj))
            {
                return redniBroj;
            }

            return -1;
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
