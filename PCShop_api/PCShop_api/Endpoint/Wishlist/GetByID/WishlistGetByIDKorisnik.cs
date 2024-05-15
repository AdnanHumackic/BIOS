
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Wishlist.GetByID
{
    [MyAuthorization]
    [Route("Wishlist-GetByID")]
    public class WishlistGetByIDKorisnik : MyBaseEndpoint<WishlistGetByIDRequest,WishlistGetByIDResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;
        public WishlistGetByIDKorisnik(ApplicationDbContext applicationDbContext, MyAuthService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService= myAuthService;
        }

        [HttpGet]
        public override async Task<WishlistGetByIDResponse> Akcija([FromQuery]WishlistGetByIDRequest request, CancellationToken cancellationToken)
        {
            var wishlist = await _applicationDbContext.Wishlist.Where(x => x.EvidentiraoKorisnikId == request.ID).Select(x => new WishlistGetByIDResponseWishlist() {
                ArtikalId = x.ArtikalID,
                ImeArtikla = x.Artikal.ImeArtikla,
                //Slika = x.Artikal.Slika,
                Cijena = x.Artikal.Cijena,
                DatumDodavanja = x.DatumDodavanja,
                Opis = x.Artikal.Opis,
                Proizvodjac = x.Artikal.Proizvodjac
            }).ToListAsync(cancellationToken);

            return new WishlistGetByIDResponse
            {
                Wishlist = wishlist
            };
        }
    }
}
