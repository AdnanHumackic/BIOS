using Microsoft.AspNetCore.Mvc;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Wishlist.Dodaj
{
    [MyAuthorization]
    [Route("Wishlist-Dodaj")]
    public class WishlistDodajEndpoint : MyBaseEndpoint<WishlistDodajRequest, WishlistDodajResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;
        public WishlistDodajEndpoint(ApplicationDbContext applicationDbContext, MyAuthService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
        }

        [HttpPost]
        public override async Task<WishlistDodajResponse> Akcija([FromBody] WishlistDodajRequest request, CancellationToken cancellationToken)
        {
            var artikal = await _applicationDbContext.Artikal.FindAsync(request.ArtikalID);

            var novaStavkaWishlist = new Data.Models.Wishlist
            {
                ArtikalID = artikal.ID,
                DatumDodavanja = DateTime.Now,
                EvidentiraoKorisnikId = _myAuthService.GetAuthInfo().korisnickiNalog!.ID
            };

            _applicationDbContext.Wishlist.Add(novaStavkaWishlist);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new WishlistDodajResponse
            {

            };
        }
    }
}
