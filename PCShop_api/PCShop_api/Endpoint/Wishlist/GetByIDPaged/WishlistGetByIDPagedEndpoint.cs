using Microsoft.AspNetCore.Mvc;
using PCShop_api.Data;
using PCShop_api.Endpoint.Artikal.GetAllKojiSuObrisaniPaged;
using PCShop_api.Endpoint.Wishlist.GetByID;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Wishlist.GetByIDPaged
{
    [MyAuthorization]
    [Route("Wishlist-GetByIDPaged")]
    public class WishlistGetByIDPagedEndpoint:MyBaseEndpoint<WishlistGetByIDPagedRequest, WishlistGetByIDPagedResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public WishlistGetByIDPagedEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public override async Task<WishlistGetByIDPagedResponse> Akcija([FromQuery]WishlistGetByIDPagedRequest request, CancellationToken cancellationToken)
        {
            var wishlist = _applicationDbContext.Wishlist.Where(x => x.EvidentiraoKorisnikId == request.ID)
                .Select(x => new WishlistGetByIDPagedResponseWishlist()
                {
                ArtikalId = x.ArtikalID,
                ImeArtikla = x.Artikal.ImeArtikla,
                Cijena = x.Artikal.Cijena,
                DatumDodavanja = x.DatumDodavanja,
                Opis = x.Artikal.Opis,
                Proizvodjac = x.Artikal.Proizvodjac
                });

            var dataOfOnePage = PagedList<WishlistGetByIDPagedResponseWishlist>.Create(wishlist, request.PageNumber, request.PageSize);
            return new WishlistGetByIDPagedResponse
            {
                Wishlist = dataOfOnePage
            };
        }
    }
}
