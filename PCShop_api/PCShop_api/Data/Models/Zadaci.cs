using System.ComponentModel.DataAnnotations.Schema;

namespace PCShop_api.Data.Models
{
    public class Zadaci
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        [ForeignKey(nameof(Radnik))]
        public int RadnikID { get; set; }
        public Radnik Radnik { get; set; }
        public DateTime DatumDodavanja { get; set; } = DateTime.Now;
        public DateTime DatumZavrsetka { get; set; }

        [ForeignKey(nameof(EvidentiraoAdmin))]
        public int AdminID { get; set; }
        public Admin EvidentiraoAdmin { get; set; }
    }
}
