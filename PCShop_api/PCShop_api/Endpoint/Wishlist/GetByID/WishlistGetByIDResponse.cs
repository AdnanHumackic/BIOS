using PCShop_api.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCShop_api.Endpoint.Wishlist.GetByID
{
    public class WishlistGetByIDResponse
    {
        public List<WishlistGetByIDResponseWishlist> Wishlist { get; set; }
    }
    public class WishlistGetByIDResponseWishlist
    {
        public int ArtikalId { get; set; }
        public string ImeArtikla { get; set; }
        public string Proizvodjac { get; set; }
        public int Cijena { get; set; }
        public string Opis { get; set; }
        //public string Slika { get; set; }
        public DateTime DatumDodavanja { get; set; } = DateTime.Now;
    }
}
