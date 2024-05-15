using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCShop_api.Data.Models
{
    public class Recenzija
    {
        [Key]
        public int ID { get; set; }
        public string Sadrzaj { get; set; }
        public DateTime DatumDodavanja { get; set; }

        [ForeignKey(nameof(EvidentiraoKorisnik))]
        public int EvidentiraoKorisnikId { get; set; }
        public KorisnickiNalog EvidentiraoKorisnik { get; set; }

        [ForeignKey(nameof(Artikal))]
        public int ArtikalId { get; set; }
        public Artikal Artikal { get; set; }
    }
}
