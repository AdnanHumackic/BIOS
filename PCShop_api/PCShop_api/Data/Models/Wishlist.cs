using System.ComponentModel.DataAnnotations.Schema;

namespace PCShop_api.Data.Models
{
    public class Wishlist
    {
        public int ID { get; set; }
        [ForeignKey(nameof(Artikal))]
        public int ArtikalID { get; set; }
        public Artikal Artikal { get; set; }
        public DateTime DatumDodavanja { get; set; } = DateTime.Now;

        [ForeignKey(nameof(EvidentiraoKorisnik))]
        public int EvidentiraoKorisnikId { get; set; }
        public KorisnickiNalog EvidentiraoKorisnik { get; set; }

    }
}
