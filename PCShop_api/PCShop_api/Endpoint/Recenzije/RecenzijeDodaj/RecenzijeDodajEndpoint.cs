using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Recenzije.RecenzijeDodaj
{
    //[MyAuthorization]
    [Route("Recenzija-Dodaj")]
    public class RecenzijeDodajEndpoint : MyBaseEndpoint<RecenzijeDodajRequest,RecenzijeDodajResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;
        public RecenzijeDodajEndpoint(ApplicationDbContext applicationDbContext, MyAuthService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
        }

        [HttpPost]
        public override async Task<RecenzijeDodajResponse> Akcija([FromBody]RecenzijeDodajRequest request, CancellationToken cancellationToken)
        {
            var artikal = await _applicationDbContext.Artikal.Where(x => x.ID == request.ArtikalId).FirstOrDefaultAsync();

            if (artikal == null)
            {
                throw new Exception("Artikal ne postoji!");
            }

            var novaRecenzija = new Data.Models.Recenzija
            {
                ID=request.ID,
                ArtikalId = artikal.ID,
                DatumDodavanja = DateTime.Now,
                Sadrzaj = request.Sadrzaj,
                EvidentiraoKorisnikId = _myAuthService.GetAuthInfo().korisnickiNalog!.ID
            };

            _applicationDbContext.Recenzije.Add(novaRecenzija);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new RecenzijeDodajResponse
            {

            };
        }
    }
}
