using System.ComponentModel.DataAnnotations.Schema;

namespace PCShop_api.Endpoint.Zadaci.Dodaj
{
    public class ZadatakDodajRequest
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public int RadnikID { get; set; }
        public DateTime DatumDodavanja { get; set; } = DateTime.Now;
        public DateTime DatumZavrsetka { get; set; }
    }
}
