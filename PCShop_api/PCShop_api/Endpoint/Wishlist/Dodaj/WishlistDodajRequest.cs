using System.ComponentModel.DataAnnotations.Schema;

namespace PCShop_api.Endpoint.Wishlist.Dodaj
{
    public class WishlistDodajRequest
    {
        public int ID { get; set; }
        public int ArtikalID { get; set; }
        public DateTime DatumDodavanja { get; set; } = DateTime.Now;
        public int EvidentiraoKorisnikId { get; set; }
    }
}
