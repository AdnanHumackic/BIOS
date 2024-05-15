using System.ComponentModel.DataAnnotations.Schema;

namespace PCShop_api.Data.Models
{
    [Table("Radnik")]
    public class Radnik:KorisnickiNalog
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }

        [ForeignKey(nameof(DrzavaPorijekla))]
        public int DrzavaID { get; set; }
        public Drzava DrzavaPorijekla { get; set; }
        public DateTime DatumRodjenja { get; set; }

        public DateTime DatumZaposlenja { get; set; }
        public string? Email { get; set; }
    }
}
