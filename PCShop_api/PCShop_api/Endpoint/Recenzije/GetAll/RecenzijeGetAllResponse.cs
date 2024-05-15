using System.ComponentModel.DataAnnotations.Schema;

namespace PCShop_api.Endpoint.Recenzije.GetAll
{
    public class RecenzijeGetAllResponse
    {
        public List<RecenzijeGetAllResponseRecenzije> Recenzije { get; set; }
    }
    public class RecenzijeGetAllResponseRecenzije
    {
        public int ID { get; set; }
        public string Sadrzaj { get; set; }
        public DateTime DatumDodavanja { get; set; }
        public string KorisnickoIme { get; set; }
        public int ArtikalId { get; set; }
    }
}
