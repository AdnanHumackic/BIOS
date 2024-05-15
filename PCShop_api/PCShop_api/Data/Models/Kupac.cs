using System.ComponentModel.DataAnnotations.Schema;

namespace PCShop_api.Data.Models
{
    [Table("Kupac")]
    public class Kupac:KorisnickiNalog
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string? Email { get; set; }

        [ForeignKey(nameof(DrzavaPorijekla))]
        public int DrzavaID { get;set; }
        public Drzava DrzavaPorijekla { get; set; }
        public DateTime DatumRodjenja { get; set; } 
    }
}
