using System.ComponentModel.DataAnnotations.Schema;

namespace PCShop_api.Data.Models
{
    public class Artikal
    {
        public int ID { get; set; }
        public string ImeArtikla { get; set; }
        public string Proizvodjac { get; set; }
        public int Cijena { get; set; }
        [ForeignKey(nameof(TipArtikla))]
        public int TipID { get; set; }
        public TipArtikla TipArtikla { get; set; }
        public string Opis { get; set; }
        public string? Slika { get; set; }

        public ICollection<Wishlist> Wishlists { get; set; }
        public ICollection<Korpa> Korpe { get; set; }
        public bool isObrisan { get; set; } = false;
    }
}
