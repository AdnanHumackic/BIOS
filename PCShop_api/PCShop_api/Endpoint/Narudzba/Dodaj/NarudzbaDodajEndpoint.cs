
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;
using PCShop_api.SignalR;

namespace PCShop_api.Endpoint.Narudzba.Dodaj
{
    [MyAuthorization]
    [Route("Narudzba-Dodaj")]
    public class NarudzbaDodajEndpoint : MyBaseEndpoint<NarudzbaDodajRequest, NarudzbaDodajResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;
        private readonly IHubContext<SignalRHub> _hubContext;
        public NarudzbaDodajEndpoint(ApplicationDbContext applicationDbContext, MyAuthService myAuthService, IHubContext<SignalRHub> hubContext)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
            _hubContext = hubContext;
        }

        [HttpPost]
        public override async Task<NarudzbaDodajResponse> Akcija([FromBody] NarudzbaDodajRequest request, CancellationToken cancellationToken)
        {
            var novaNarudzba = new Data.Models.Narudzba
            {
                ID = request.ID,
                Ime = request.Ime,
                Prezime = request.Prezime,
                Adresa = request.Adresa,
                Dostavljac = request.Dostavljac,
                BrojTelefona = request.BrojTelefona,
                UkupnaCijena = request.UkupnaCijena,
                EvidentiraoKorisnikId = _myAuthService.GetAuthInfo().korisnickiNalog!.ID
            };
            _applicationDbContext.Narudzba.Add(novaNarudzba);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var radnik = await _applicationDbContext.Radnik.Select(x => x.KorisnickoIme).ToListAsync();

            foreach (var radn in radnik)
            {
                await _hubContext.Clients.Group(radn).SendAsync("prijem_poruke_js" ,novaNarudzba.EvidentiraoKorisnik.KorisnickoIme
                    + " je upravo kreirao narudzbu!", cancellationToken);
            }

            return new NarudzbaDodajResponse { };
        }
    }
}
