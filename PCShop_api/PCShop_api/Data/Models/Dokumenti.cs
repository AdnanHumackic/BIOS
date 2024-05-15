using System.ComponentModel.DataAnnotations.Schema;

namespace PCShop_api.Data.Models
{
    public class Dokumenti
    {
        public int ID { get; set; }
        public string? NazivFajla { get; set; }
        public string? SifraFajla { get; set; }

        [ForeignKey(nameof(EvidentiraoKorisnik))]
        public int EvidentiraoKorisnikID { get; set; }
        public KorisnickiNalog EvidentiraoKorisnik { get; set; }

    }
}
