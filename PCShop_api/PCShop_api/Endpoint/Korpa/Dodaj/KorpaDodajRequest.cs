using System.ComponentModel.DataAnnotations.Schema;

namespace PCShop_api.Endpoint.Korpa.Dodaj
{
    public class KorpaDodajRequest
    {
        public int ID { get; set; }
        public DateTime DatumDodavanja { get; set; }
        public int ArtikalID { get; set; }
        public int EvidentiraoKorisnikId { get; set; }
    }
}
