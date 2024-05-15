using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Data.Models;

namespace PCShop_api.Endpoint.GeneratorPodatakaEndpoint
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PodaciController:ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PodaciController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public ActionResult Count()
        {
            Dictionary<string, int> data = new Dictionary<string, int>();
            data.Add("Drzava", _applicationDbContext.Drzava.Count());
            data.Add("Radnik", _applicationDbContext.Radnik.Count());
            data.Add("Kupac", _applicationDbContext.Kupac.Count());
            data.Add("Admin", _applicationDbContext.Admin.Count());
            data.Add("Tipovi", _applicationDbContext.TipArtikla.Count());

            return Ok(data);
        }

        [HttpPost]
        public ActionResult Generisi()
        {
            var drzave = new List<Data.Models.Drzava>();
            var radnici = new List<Data.Models.Radnik>();
            var admini = new List<Data.Models.Admin>();
            var kupci = new List<Data.Models.Kupac>();
            var tipovi = new List<Data.Models.TipArtikla>();

            drzave.Add(new Data.Models.Drzava { Naziv = "BiH" });
            drzave.Add(new Data.Models.Drzava { Naziv = "HR" });
            drzave.Add(new Data.Models.Drzava { Naziv = "Njemacka" });
            drzave.Add(new Data.Models.Drzava { Naziv = "Austrija" });
            drzave.Add(new Data.Models.Drzava { Naziv = "SAD" });
            drzave.Add(new Data.Models.Drzava { Naziv = "Malezija" });

            tipovi.Add(new Data.Models.TipArtikla { Tip = "Grafička kartica" });
            tipovi.Add(new Data.Models.TipArtikla { Tip = "Matična ploča" });
            tipovi.Add(new Data.Models.TipArtikla { Tip = "Procesor" });
            tipovi.Add(new Data.Models.TipArtikla { Tip = "------" });


            radnici.Add(new Data.Models.Radnik { Is2FActive = false, Ime = "Adnan", Prezime = "Humačkić", KorisnickoIme = "adnan", Lozinka = "test", SlikaKorisnika = null,
            DatumRodjenja=DateTime.Now, DatumZaposlenja=DateTime.Now, DrzavaPorijekla = drzave[2]
            });
            radnici.Add(new Data.Models.Radnik { Is2FActive = false, Ime = "Radnik", Prezime = "Tester", KorisnickoIme = "radnik", Lozinka = "test", SlikaKorisnika = null, 
                DatumRodjenja = DateTime.Now, DatumZaposlenja = DateTime.Now, DrzavaPorijekla = drzave[1],
            });

            admini.Add(new Data.Models.Admin { Is2FActive = false, Ime = "Omar", Prezime = "Čolakhodžić", KorisnickoIme = "omar", Lozinka = "test", SlikaKorisnika = null, 
            DatumRodjenja=DateTime.Now, DatumZaposlenja=DateTime.Now, DrzavaPorijekla = drzave[1]
            });
            admini.Add(new Data.Models.Admin {Is2FActive = false, Ime = "Admin", Prezime = "Tester", KorisnickoIme = "admin", Lozinka = "test", SlikaKorisnika = null, 
                DatumRodjenja = DateTime.Now, DatumZaposlenja = DateTime.Now, DrzavaPorijekla = drzave[1] });

            kupci.Add(new Data.Models.Kupac { Is2FActive = false, Ime = "Ensar", Prezime = "Čevra", KorisnickoIme = "ensar", Lozinka = "test", SlikaKorisnika = null,
                DrzavaPorijekla = drzave[4], DatumRodjenja=DateTime.Now});
            kupci.Add(new Data.Models.Kupac { Is2FActive = false, Ime = "Kupac", Prezime = "Tester", KorisnickoIme = "kupac", Lozinka = "test", SlikaKorisnika = null,
                DrzavaPorijekla = drzave[3], DatumRodjenja=DateTime.Now});


            _applicationDbContext.AddRange(radnici);
            _applicationDbContext.AddRange(admini);
            _applicationDbContext.AddRange(kupci);
            _applicationDbContext.AddRange(drzave);
            _applicationDbContext.AddRange(tipovi);

            _applicationDbContext.SaveChanges();

            return Count();
        }
    }
}
