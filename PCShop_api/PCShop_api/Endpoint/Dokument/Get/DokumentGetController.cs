using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Endpoint.Artikal.GetAllKojiSuObrisaniPaged;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;
using PCShop_api.SignalR;
using System.Threading;

namespace PCShop_api.Endpoint.Dokument.Get
{
    [MyAuthorization]
    [Route("[controller]")]
    [ApiController]
    public class DokumentGetController:ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _environment;


        public DokumentGetController(ApplicationDbContext applicationDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _applicationDbContext = applicationDbContext;
            _environment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult GetDokument([FromQuery] DokumentGetDto obj)
        {
            var filesDirectory = Path.Combine(_environment.WebRootPath, "Fajlovi", "Dokumenti");
            var files = Directory.GetFiles(filesDirectory);

            var podaci = files.Select(file => new DokumentResponse
            {
                FileUrl = Path.Combine("https://localhost:7201/Fajlovi/Dokumenti", Path.GetFileName(file)),
                Naziv = Path.GetFileNameWithoutExtension(file)
            }).AsQueryable();

            var dataOfOnePage = PagedList<DokumentResponse>.Create(podaci, obj.PageNumber, obj.PageSize);

            return Ok(dataOfOnePage);
        }

        private static string GetFile(string path, IWebHostEnvironment env)
        {
            string imageUrl = string.Empty;
            string HostUrl = "https://localhost:7201";
            string filepath = GetFilePath(path, env);
            if (!System.IO.File.Exists(filepath))
            {
                imageUrl = "";
            }
            else
            {
                imageUrl = HostUrl + "/Fajlovi/Dokumenti/" + path;
            }

            return imageUrl;
        }

        private static string GetFilePath(string productCode, IWebHostEnvironment env)
        {
            return env.WebRootPath + "\\Fajlovi\\Dokumenti\\" + productCode;
        }
    }

    public class DokumentGetDto
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }

    public class DokumentResponse
    {
        public string FileUrl { get; set; }
        public string Naziv { get; set; }
    }
   

}

