using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Wishlist.Obrisi
{
    [MyAuthorization]
    [Route("Wishlist-Obrisi")]
    public class WishlistObrisiEndpoint : MyBaseEndpoint<WishlistObrisiRequest, WishlistObrisiResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public WishlistObrisiEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpDelete]
        public override async Task<WishlistObrisiResponse> Akcija([FromQuery] WishlistObrisiRequest request, CancellationToken cancellationToken)
        {
            var wishlistItem = await _applicationDbContext.Wishlist.Where(x => x.ArtikalID == request.ID).FirstOrDefaultAsync(cancellationToken);

            if (wishlistItem == null)
            {
                throw new Exception("Nije pronadjen ID za artikal: " + request.ID);
            }

            _applicationDbContext.Wishlist.Remove(wishlistItem);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new WishlistObrisiResponse();
        }
    }
}
