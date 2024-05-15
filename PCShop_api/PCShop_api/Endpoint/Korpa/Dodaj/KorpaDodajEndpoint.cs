
using Microsoft.AspNetCore.Mvc;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Korpa.Dodaj
{
    [MyAuthorization]
    [Route("Korpa-Dodaj")]
    public class KorpaDodajEndpoint : MyBaseEndpoint<KorpaDodajRequest, KorpaDodajResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;

        public KorpaDodajEndpoint(ApplicationDbContext applicationDbContext, MyAuthService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
        }

        [HttpPost]
        public override async Task<KorpaDodajResponse> Akcija([FromBody] KorpaDodajRequest request, CancellationToken cancellationToken)
        {
            var artikal = await _applicationDbContext.Artikal.FindAsync(request.ArtikalID);

            var novaStavkaKorpa = new Data.Models.Korpa
            {
                ArtikalID = artikal.ID,
                DatumDodavanja = DateTime.Now,
                EvidentiraoKorisnikId = _myAuthService.GetAuthInfo().korisnickiNalog!.ID
            };

            _applicationDbContext.Korpa.Add(novaStavkaKorpa);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new KorpaDodajResponse
            {

            };
        }
    }
}
