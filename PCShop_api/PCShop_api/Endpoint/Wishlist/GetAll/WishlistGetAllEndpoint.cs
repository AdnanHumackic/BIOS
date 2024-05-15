using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;
using static PCShop_api.Endpoint.Wishlist.GetAll.WishlistGetAllResponse;

namespace PCShop_api.Endpoint.Wishlist.GetAll
{
    [MyAuthorization]
    [Route("Wishlist-GetAll")]
    public class WishlistGetAllEndpoint : MyBaseEndpoint<WishlistGetAllRequest, WishlistGetAllResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public WishlistGetAllEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public override async Task<WishlistGetAllResponse> Akcija([FromQuery] WishlistGetAllRequest request, CancellationToken cancellationToken)
        {
            var wishlistLista = await _applicationDbContext.Wishlist
                .Select(x => new WishlistGetAllResponseWishlist()
                {
                    ArtikalId = x.ArtikalID,
                    ImeArtikla = x.Artikal.ImeArtikla,
                    Proizvodjac = x.Artikal.Proizvodjac,
                    Cijena = x.Artikal.Cijena,
                    Opis = x.Artikal.Opis,
                    //Slika = x.Artikal.Slika,
                    DatumDodavanja = x.DatumDodavanja
                }).ToListAsync(cancellationToken);

            return new WishlistGetAllResponse
            {
                StavkeWishlist = wishlistLista
            };
        }
    }
}
