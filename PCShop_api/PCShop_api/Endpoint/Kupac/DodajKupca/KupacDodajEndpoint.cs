using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.SignalR;

namespace PCShop_api.Endpoint.Kupac.DodajKupca
{
    [Route("Kupac-Dodaj")]
    public class KupacDodajEndpoint : MyBaseEndpoint<KupacDodajRequest,KupacDodajResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHubContext<SignalRHub> _hubContext;
        public KupacDodajEndpoint(ApplicationDbContext applicationDbContext,IHubContext<SignalRHub>hubContext)
        {
            _applicationDbContext = applicationDbContext;
            _hubContext = hubContext;
        }

        [HttpPost]
        public override async Task<KupacDodajResponse> Akcija([FromBody]KupacDodajRequest request, CancellationToken cancellationToken)
        {
            var noviKupac = new Data.Models.Kupac
            {
                ID = request.ID,
                Ime = request.Ime,
                Prezime = request.Prezime,
                DrzavaID = request.Drzava,
                Lozinka = request.Lozinka,
                KorisnickoIme = request.KorisnickoIme,
                DatumRodjenja = request.DatumRodjenja,
                Email=request.Email,
            };
            _applicationDbContext.Kupac.Add(noviKupac);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var administratori = await _applicationDbContext.Admin.Select(x => x.KorisnickoIme).ToListAsync();

            foreach (var admin in administratori)
            {
                await _hubContext.Clients.Group(admin).SendAsync("prijem_poruke_js", "Upravo se registrovao novi korisnik!", cancellationToken);
            }

            return new KupacDodajResponse
            {

            };
        }
    }
}
