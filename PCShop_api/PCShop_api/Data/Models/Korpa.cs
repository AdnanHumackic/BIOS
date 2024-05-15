using System.ComponentModel.DataAnnotations.Schema;

namespace PCShop_api.Data.Models
{
    public class Korpa
    {
        public int ID { get; set; }
        public DateTime DatumDodavanja { get; set; }
 

        [ForeignKey(nameof(Artikal))]
        public int ArtikalID { get; set; }
        public Artikal Artikal { get; set; }

        [ForeignKey(nameof(EvidentiraoKorisnik))]
        public int EvidentiraoKorisnikId { get; set; }
        public KorisnickiNalog EvidentiraoKorisnik { get; set; }
    }
}
