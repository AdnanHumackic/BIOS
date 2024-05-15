using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;
using PCShop_api.SignalR;

namespace PCShop_api.Endpoint.Dokument.Dodaj
{
    [MyAuthorization]   
    [Route("controller")]
    [ApiController]
    public class DokumentDodajController:ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _environment;
        private readonly MyAuthService _myAuth;
        private readonly IHubContext<SignalRHub> _hubContext;

        public DokumentDodajController(ApplicationDbContext applicationDbContext, IWebHostEnvironment environment, 
            MyAuthService myAuth, IHubContext<SignalRHub> hubContext)
        {
            _applicationDbContext = applicationDbContext;
            _environment = environment;
            _myAuth = myAuth;
            _hubContext = hubContext;
        }

        [HttpPost]                  
        public async Task<ActionResult> PostFile([FromForm]AddFileBody obj)
        {
            if (obj != null)
            {
                string projekatFolder1 = Environment.CurrentDirectory;
                string orginalniNaziv = obj.File.FileName;
                string fileName = Path.GetFileName(orginalniNaziv);

                string envFile = Path.Combine(_environment.WebRootPath, "Fajlovi", "Dokumenti", fileName);

                using (Stream fileStream = new FileStream(envFile, FileMode.Create))
                {
                    obj.File.CopyTo(fileStream);
                }

                var dokument = new Data.Models.Dokumenti();

                if (dokument != null)
                {
                    dokument.NazivFajla = orginalniNaziv;
                    dokument.SifraFajla = fileName;
                    dokument.EvidentiraoKorisnikID = _myAuth.GetAuthInfo().korisnickiNalog!.ID;
                }

                _applicationDbContext.SaveChanges();
                var pdfToText = PdfToWord.ConvertToString(fileName, _environment);

                var radnici = _applicationDbContext.Radnik.Select(x => x.KorisnickoIme).ToList();
                foreach (var radnik in radnici)
                {
                    _hubContext.Clients.Group(radnik).SendAsync("prijem_poruke_js", "Molimo da posjetite sekciju za dokumente!");
                }

                return Ok(new Response() { Tekst = pdfToText });

            }

            return BadRequest();
        }
    }
}
    public class Response
    {
        public string Tekst { get; set; }
    }

    public class AddFileBody
    {
        public IFormFile File { get; set; }

    }
