using Microsoft.AspNetCore.Mvc;
using PCShop_api.Data;
using PCShop_api.Helper;

namespace PCShop_api.Endpoint.Uloga.PrebaciUAdmin
{
    [Route("Uloga-Admin")]
    public class PrebaciUAdminEndpoint:MyBaseEndpoint<PrebaciUAdminRequest,PrebaciUAdminResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PrebaciUAdminEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<PrebaciUAdminResponse> Akcija([FromBody]PrebaciUAdminRequest request, CancellationToken cancellationToken)
        {
            var korisnickiNalog = await _applicationDbContext.KorisnickiNalog.FindAsync(request.ID);
            if (korisnickiNalog == null)
            {
                throw new Exception("Korisnik ne postoji!");
            }

            if (korisnickiNalog.isAdmin)
            {
                throw new Exception("Korisnik je vec admin!");
            }
            else if (korisnickiNalog.isKupac)
            {
                var kupac = korisnickiNalog as Data.Models.Kupac;

                var recenzijeZaBrisanje = _applicationDbContext.Recenzije.Where(x => x.EvidentiraoKorisnikId == kupac.ID);
                foreach (var recenzija in recenzijeZaBrisanje)
                {
                    _applicationDbContext.Recenzije.Remove(recenzija);
                }

                var sadrzajKorpe = _applicationDbContext.Korpa.Where(x => x.EvidentiraoKorisnikId == kupac.ID);
                foreach (var sadrzaj in sadrzajKorpe)
                {
                    _applicationDbContext.Korpa.Remove(sadrzaj);
                }

                var sadrzajWishlista = _applicationDbContext.Wishlist.Where(x => x.EvidentiraoKorisnikId == kupac.ID);
                foreach (var wish in sadrzajWishlista)
                {
                    _applicationDbContext.Wishlist.Remove(wish);
                }

                var sadrzajNarudzbe = _applicationDbContext.Narudzba.Where(x => x.EvidentiraoKorisnikId == kupac.ID);
                foreach (var narudzba in sadrzajNarudzbe)
                {
                    _applicationDbContext.Narudzba.Remove(narudzba);
                }

                await _applicationDbContext.SaveChangesAsync();

                var noviAdmin = new Data.Models.Admin
                {
                    Ime = kupac.Ime,
                    Prezime = kupac.Prezime,
                    DrzavaID = kupac.DrzavaID,
                    DrzavaPorijekla = kupac.DrzavaPorijekla,
                    DatumRodjenja = kupac.DatumRodjenja,
                    DatumZaposlenja = DateTime.Now,
                    Email = kupac.Email,
                    KorisnickoIme = kupac.KorisnickoIme,
                    Lozinka = kupac.Lozinka,
                    SlikaKorisnika = kupac.SlikaKorisnika
                };

                _applicationDbContext.Admin.Add(noviAdmin);
                _applicationDbContext.Kupac.Remove(kupac);
                await _applicationDbContext.SaveChangesAsync();
            }
            else if (korisnickiNalog.isRadnik)
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

                var noviAdmin = new Data.Models.Admin
                {
                    Ime = radnik.Ime,
                    Prezime = radnik.Prezime,
                    DrzavaID = radnik.DrzavaID,
                    DrzavaPorijekla = radnik.DrzavaPorijekla,
                    DatumRodjenja = radnik.DatumRodjenja,
                    DatumZaposlenja = DateTime.Now,
                    Email = radnik.Email,
                    KorisnickoIme = radnik.KorisnickoIme,
                    Lozinka = radnik.Lozinka,
                    SlikaKorisnika = radnik.SlikaKorisnika
                };

                _applicationDbContext.Admin.Add(noviAdmin);
                _applicationDbContext.Radnik.Remove(radnik);
                await _applicationDbContext.SaveChangesAsync();
            }

            return new PrebaciUAdminResponse
            {

            };
        }
    }
}
