using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Endpoint.Wishlist.GetByID;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Korpa.GetByID
{
    [MyAuthorization]
    [Route("Korpa-GetByID")]
    public class KorpaGetByIDKorisnik : MyBaseEndpoint<KorpaGetByIDRequest, KorpaGetByIDResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;
        public KorpaGetByIDKorisnik(ApplicationDbContext applicationDbContext, MyAuthService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
        }

        [HttpGet]
        public override async Task<KorpaGetByIDResponse> Akcija([FromQuery]KorpaGetByIDRequest request, CancellationToken cancellationToken)
        {
            var korpa = await _applicationDbContext.Korpa.Where(x => x.EvidentiraoKorisnikId == request.ID).Select(x => new KorpaGetByIDResponseKorpa() {
                ArtikalID = x.ArtikalID,
                ImeArtikla = x.Artikal.ImeArtikla,
                //Slika = x.Artikal.Slika,
                Cijena = x.Artikal.Cijena,
                DatumDodavanja = x.DatumDodavanja,
                Opis = x.Artikal.Opis,
                ID = x.ID
            }).ToListAsync(cancellationToken);

            return new KorpaGetByIDResponse
            {
                Korpa= korpa
            };
        }
    }
}
