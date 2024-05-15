using PCShop_api.Helper;

namespace PCShop_api.Endpoint.Wishlist.GetByIDPaged
{
    public class WishlistGetByIDPagedResponse
    {
        public PagedList<WishlistGetByIDPagedResponseWishlist> Wishlist { get; set; }
    }
    public class WishlistGetByIDPagedResponseWishlist
    {
        public int ArtikalId { get; set; }
        public string ImeArtikla { get; set; }
        public string Proizvodjac { get; set; }
        public int Cijena { get; set; }
        public string Opis { get; set; }
        public DateTime DatumDodavanja { get; set; } = DateTime.Now;
    }

}
