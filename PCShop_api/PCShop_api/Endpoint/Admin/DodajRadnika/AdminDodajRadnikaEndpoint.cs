using Microsoft.AspNetCore.Mvc;
using PCShop_api.Data;
using PCShop_api.Endpoint.Artikal.Dodaj;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Admin.DodajRadnika
{
    [MyAuthorization]
    [Route("Admin-DodajRadnika")]
    public class AdminDodajRadnikaEndpoint:MyBaseEndpoint<AdminDodajRadnikaRequest, AdminDodajRadnikaResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AdminDodajRadnikaEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<AdminDodajRadnikaResponse> Akcija([FromBody]AdminDodajRadnikaRequest request, CancellationToken cancellationToken)
        {
            var noviRadnik = new Data.Models.Radnik
            {
                ID = request.ID,
                Ime = request.Ime,
                Prezime = request.Prezime,
                DrzavaID = request.Drzava,
                Lozinka = request.Lozinka,
                KorisnickoIme = request.KorisnickoIme,
                DatumRodjenja = request.DatumRodjenja,
                DatumZaposlenja = DateTime.Now,
                Email=request.Email
            };

            _applicationDbContext.Radnik.Add(noviRadnik);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new AdminDodajRadnikaResponse
            {

            };
        }
    }
}
