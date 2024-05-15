using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Data.Models;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;
using PCShop_api.SignalR;

namespace PCShop_api.Endpoint.Zadaci.Obrisi
{
    [MyAuthorization]
    [Route("Zadaci-Obrisi")]
    public class ZadaciObrisiEndpoint:MyBaseEndpoint<ZadaciObrisiRequest,ZadaciObrisiResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHubContext<SignalRHub> _hubContext;
        private readonly MyAuthService _myAuth;
        public ZadaciObrisiEndpoint(ApplicationDbContext applicationDbContext, IHubContext<SignalRHub> hubContext,
            MyAuthService myAuth)
        {
            _applicationDbContext = applicationDbContext;
            _hubContext = hubContext;
            _myAuth = myAuth;
        }
        [HttpDelete]
        public override async Task<ZadaciObrisiResponse> Akcija([FromQuery] ZadaciObrisiRequest request, CancellationToken cancellationToken)
        {
            var zadatakItem = await _applicationDbContext.Zadatak.Where(x => x.Id == request.ID).FirstOrDefaultAsync(cancellationToken);
            
            if (zadatakItem == null)
            {
                throw new Exception("Nije pronadjen Id za artikal: " + request.ID);
            }

            var radnik = await _applicationDbContext.Radnik.Where(x => x.ID == zadatakItem.RadnikID)
                .Select(x => x.KorisnickoIme).FirstOrDefaultAsync();

            var admin = await _applicationDbContext.Zadatak.Where(x => x.AdminID == zadatakItem.AdminID)
           .Select(x => x.EvidentiraoAdmin.KorisnickoIme).FirstOrDefaultAsync();
            _applicationDbContext.Zadatak.Remove(zadatakItem);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            
            if (admin != null)
            {
                await _hubContext.Clients.Group(admin).SendAsync("prijem_poruke_js", radnik+" je uspješno obavio " +
                    "dodijeljeni zadatak!", cancellationToken);
            }

            return new ZadaciObrisiResponse();
        }
    }
}
