using System.ComponentModel.DataAnnotations.Schema;

namespace PCShop_api.Data.Models
{
    public class Narudzba
    {
        public int ID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Adresa { get; set; }
        public string BrojTelefona { get; set; }
        public string Dostavljac { get; set; }
        public float UkupnaCijena { get; set; }
        [ForeignKey(nameof(EvidentiraoKorisnik))]
        public int EvidentiraoKorisnikId { get; set; }
        public KorisnickiNalog EvidentiraoKorisnik { get; set; }
    }
}
