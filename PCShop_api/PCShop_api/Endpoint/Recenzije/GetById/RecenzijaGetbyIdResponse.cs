using System.ComponentModel.DataAnnotations.Schema;

namespace PCShop_api.Endpoint.Recenzije.GetById
{
    public class RecenzijaGetbyIdResponse
    {
        public List<RecenzijaGetbyIdResponseRecenzija> Recenzija { get; set; }
    }
    public class RecenzijaGetbyIdResponseRecenzija
    {
        public int ID { get; set; }
        public string Sadrzaj { get; set; }
        public DateTime DatumDodavanja { get; set; }
        public int EvidentiraoKorisnikId { get; set; }
        public int ArtikalId { get; set; }
        public string KorisnickoIme { get; set; }
    }
}
