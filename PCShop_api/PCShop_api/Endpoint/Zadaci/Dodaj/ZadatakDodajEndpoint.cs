using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;
using PCShop_api.SignalR;

namespace PCShop_api.Endpoint.Zadaci.Dodaj
{
    [MyAuthorization]
    [Route("Zadatak-Dodaj")]
    public class ZadatakDodajEndpoint : MyBaseEndpoint<ZadatakDodajRequest, ZadatakDodajResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;
        private readonly IHubContext<SignalRHub> _hubContext;


        public ZadatakDodajEndpoint(ApplicationDbContext applicationDbContext, MyAuthService myAuthService,
            IHubContext<SignalRHub> hubContext)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
            _hubContext = hubContext;
        }

        [HttpPost]
        public override async Task<ZadatakDodajResponse> Akcija([FromBody] ZadatakDodajRequest request, CancellationToken cancellationToken)
        {
            var noviZadatak = new Data.Models.Zadaci
            {
                Id = request.Id,
                Naziv = request.Naziv,
                Opis = request.Opis,
                DatumDodavanja = request.DatumDodavanja,
                DatumZavrsetka = request.DatumZavrsetka,
                RadnikID = request.RadnikID,
                AdminID=_myAuthService.GetAuthInfo().korisnickiNalog!.ID
            };

            await _applicationDbContext.Zadatak.AddAsync(noviZadatak);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var admin = await _applicationDbContext.Admin.Where(x => x.ID == noviZadatak.AdminID)
                .Select(x => x.KorisnickoIme).FirstOrDefaultAsync();

            var radnik = await _applicationDbContext.Zadatak.Where(x => x.RadnikID == request.RadnikID)
              .Select(x => x.Radnik.KorisnickoIme).FirstOrDefaultAsync();
            if (radnik != null)
            {
                await _hubContext.Clients.Group(radnik).SendAsync("prijem_poruke_js", "Dodijeljen vam je novi zadatak " +
                    "od strane "+admin+ " Molimo " +
                    "da posjetite sekciju za zadatke.", cancellationToken);
            }

            return new ZadatakDodajResponse
            {

            };
        }
    }
}
