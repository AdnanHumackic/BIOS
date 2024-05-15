
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;
using PCShop_api.SignalR;
using System.Runtime.ExceptionServices;

namespace PCShop_api.Endpoint.Narudzba.Obrisi
{
    [MyAuthorization]
    [Route("Narudzba-Obrisi")]
    public class NarudzbaObrisiEndpoint : MyBaseEndpoint<NarudzbaObrisiRequest, NarudzbaObrisiResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHubContext<SignalRHub> _hubContext;
        public NarudzbaObrisiEndpoint(ApplicationDbContext applicationDbContext, IHubContext<SignalRHub>hubContext)
        {
            _applicationDbContext = applicationDbContext;
            _hubContext = hubContext;
        }

        [HttpDelete]
        public override async Task<NarudzbaObrisiResponse> Akcija([FromQuery] NarudzbaObrisiRequest request, CancellationToken cancellationToken)
        {
            var narudzbe = _applicationDbContext.Narudzba.Where(x => x.ID == request.ID).FirstOrDefault();

            if (narudzbe == null)
            {
                throw new Exception("Nije pronadjena narudzba za ID: " + request.ID);
            }

            var radnik = await _applicationDbContext.Radnik.Select(x => x.KorisnickoIme).ToListAsync();
            var kupac = await _applicationDbContext.Narudzba.Where(x => x.ID == request.ID)
                .Select(x=>x.EvidentiraoKorisnik.KorisnickoIme).FirstOrDefaultAsync();
            foreach (var radn in radnik)
            {
                await _hubContext.Clients.Group(radn).SendAsync("prijem_poruke_js", kupac
                    + " je potvrdio isporuku narudzbe!", cancellationToken);
            }

            _applicationDbContext.Remove(narudzbe);
            await _applicationDbContext.SaveChangesAsync();


            return new NarudzbaObrisiResponse
            {

            };
        }
    }
}
