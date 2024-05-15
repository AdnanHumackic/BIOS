using Microsoft.AspNetCore.Mvc;
using PCShop_api.Data;
using PCShop_api.Data.Models;
using PCShop_api.Helper;

namespace PCShop_api.Endpoint.Uloga.PrebaciUKupac
{
    [Route("Uloga-Kupac")]
    public class PrebaciUKupacEndpoint:MyBaseEndpoint<PrebaciUKupacRequest,PrebaciUKupacResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PrebaciUKupacEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<PrebaciUKupacResponse> Akcija([FromBody]PrebaciUKupacRequest request, CancellationToken cancellationToken)
        {
            var korisnickiNalog = await _applicationDbContext.KorisnickiNalog.FindAsync(request.ID);
            if (korisnickiNalog == null)
            {
                throw new Exception("Korisnik ne postoji!");
            }

            if (korisnickiNalog.isKupac)
            {
                throw new Exception("Korisnik je vec kupac!");
            }
            else if(korisnickiNalog.isRadnik)
            {
                var radnik = korisnickiNalog as Data.Models.Radnik;

                var aktivniZadaci = _applicationDbContext.Zadatak.Where(x => x.RadnikID == radnik.ID);
                foreach (var zadatak in aktivniZadaci)
                {
                    _applicationDbContext.Zadatak.Remove(zadatak);
                }

                var sadrzajKorpe = _applicationDbContext.Korpa.Where(x => x.EvidentiraoKorisnikId == radnik.ID);
                foreach (var sadrzaj in sadrzajKorpe)
                {
                    _applicationDbContext.Korpa.Remove(sadrzaj);
                }

                var sadrzajWishlista = _applicationDbContext.Wishlist.Where(x => x.EvidentiraoKorisnikId == radnik.ID);
                foreach (var wish in sadrzajWishlista)
                {
                    _applicationDbContext.Wishlist.Remove(wish);
                }

                var sadrzajNarudzbe = _applicationDbContext.Narudzba.Where(x => x.EvidentiraoKorisnikId == radnik.ID);
                foreach (var narudzba in sadrzajNarudzbe)
                {
                    _applicationDbContext.Narudzba.Remove(narudzba);
                }

                var noviKupac = new Data.Models.Kupac
                {
                    Ime = radnik.Ime,
                    Prezime = radnik.Prezime,
                    DrzavaID = radnik.DrzavaID,
                    DrzavaPorijekla = radnik.DrzavaPorijekla,
                    DatumRodjenja = radnik.DatumRodjenja,
                    Email = radnik.Email,
                    KorisnickoIme = radnik.KorisnickoIme,
                    Lozinka = radnik.Lozinka,
                    SlikaKorisnika = radnik.SlikaKorisnika
                };

                _applicationDbContext.Kupac.Add(noviKupac);
                _applicationDbContext.Radnik.Remove(radnik);
                await _applicationDbContext.SaveChangesAsync();
            }
            else if (korisnickiNalog.isAdmin)
            {
                var admin = korisnickiNalog as Data.Models.Admin;

                var aktivniZadaci = _applicationDbContext.Zadatak.Where(x => x.AdminID == admin.ID);
                foreach (var zadatak in aktivniZadaci)
                {
                    _applicationDbContext.Zadatak.Remove(zadatak);
                }

                var noviKupac = new Data.Models.Kupac
                {
                    Ime = admin.Ime,
                    Prezime = admin.Prezime,
                    DrzavaID = admin.DrzavaID,
                    DrzavaPorijekla = admin.DrzavaPorijekla,
                    DatumRodjenja = admin.DatumRodjenja,
                    Email = admin.Email,
                    KorisnickoIme = admin.KorisnickoIme,
                    Lozinka = admin.Lozinka,
                    SlikaKorisnika = admin.SlikaKorisnika
                };

                _applicationDbContext.Kupac.Add(noviKupac);
                _applicationDbContext.Admin.Remove(admin);
                await _applicationDbContext.SaveChangesAsync();
            }

            return new PrebaciUKupacResponse()
            {

            };
        }
    }
}
