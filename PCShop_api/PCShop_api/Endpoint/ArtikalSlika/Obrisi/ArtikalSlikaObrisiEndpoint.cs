using Microsoft.AspNetCore.Mvc;
using PCShop_api.Data;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.ArtikalSlika.Obrisi
{
    [MyAuthorization]
    [Route("ArtikalSlika-Obrisi")]
    public class ArtikalSlikaObrisiEndpoint:ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ArtikalSlikaObrisiEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpDelete]
        public async Task<IActionResult>DeleteSlika(int artikalId, string redniBrojSlike, CancellationToken cancellationToken)
        {
            try
            {
                var folderPath = "slike-artikla";
                var velikaSlika= $"{artikalId}-{redniBrojSlike}-velika.jpg";
                var malaSlika= $"{artikalId}-{redniBrojSlike}-mala.jpg";

                var velikaFilePath = Path.Combine(folderPath, velikaSlika);
                var malaFilePath = Path.Combine(folderPath, malaSlika);

                if (System.IO.File.Exists(velikaFilePath))
                {
                    System.IO.File.Delete(velikaFilePath);
                }
                if (System.IO.File.Exists(malaFilePath))
                {
                    System.IO.File.Delete(malaFilePath);
                }
                await _applicationDbContext.SaveChangesAsync(cancellationToken);

                return Ok();
            }
            catch(Exception ex)
            {
                throw new Exception("Nema slike i artikla za proslijedjene ID-ove!");
            }
        }
    }
}
