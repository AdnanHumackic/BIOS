using Microsoft.AspNetCore.Mvc;
using PCShop_api.Data;
using PCShop_api.Data.Models;
using PCShop_api.Helper;

namespace PCShop_api.Endpoint.Uloga.PrebaciUAdmin
{
    [Route("Uloga-Radnik")]
    public class PrebaciURadnikEndpoint:MyBaseEndpoint<PrebaciURadnikRequest,PrebaciURadnikResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PrebaciURadnikEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<PrebaciURadnikResponse> Akcija([FromBody]PrebaciURadnikRequest request, CancellationToken cancellationToken)
        {
            var korisnickiNalog = await _applicationDbContext.KorisnickiNalog.FindAsync(request.ID);
            if(korisnickiNalog== null)
            {
                throw new Exception("Korisnik ne postoji!");
            }

            if(korisnickiNalog.isRadnik)
            {
                throw new Exception("Korisnik je vec radnik!");
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

                var noviRadnik = new Data.Models.Radnik
                {
                    Ime = kupac.Ime,
                    Prezime = kupac.Prezime,
                    Email = kupac.Email,
                    DrzavaID = kupac.DrzavaID,
                    DrzavaPorijekla = kupac.DrzavaPorijekla,
                    DatumRodjenja = kupac.DatumRodjenja,
                    DatumZaposlenja = DateTime.Now,
                    KorisnickoIme=kupac.KorisnickoIme,
                    Lozinka=kupac.Lozinka,
                    SlikaKorisnika=kupac.SlikaKorisnika
                };
                _applicationDbContext.Radnik.Add(noviRadnik);

                _applicationDbContext.Kupac.Remove(kupac);

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

                var noviRadnik = new Data.Models.Radnik
                {
                   Ime=admin.Ime,
                   Prezime=admin.Prezime,
                   DrzavaID=admin.DrzavaID,
                   DrzavaPorijekla=admin.DrzavaPorijekla,
                   DatumRodjenja=admin.DatumRodjenja,
                   DatumZaposlenja=admin.DatumZaposlenja,
                   Email=admin.Email,
                   KorisnickoIme=admin.KorisnickoIme,
                   Lozinka=admin.Lozinka,
                   SlikaKorisnika=admin.SlikaKorisnika
                };
                _applicationDbContext.Radnik.Add(noviRadnik);

                _applicationDbContext.Admin.Remove(admin);

                await _applicationDbContext.SaveChangesAsync();
            }

            return new PrebaciURadnikResponse
            {

            };
        }

    }
}
