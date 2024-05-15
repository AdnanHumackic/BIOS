using PCShop_api.Helper;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCShop_api.Endpoint.Recenzije.RecenzijeGetByIDPaged
{
    public class RecenzijeGetByIDPagedResponse
    {
        public PagedList<RecenzijeGetByIDPagedResponseRecenzije> Recenzije { get; set; }
    }
    public class RecenzijeGetByIDPagedResponseRecenzije
    {
        public int ID { get; set; }
        public string Sadrzaj { get; set; }
        public DateTime DatumDodavanja { get; set; }
        public int EvidentiraoKorisnikId { get; set; }
        public int ArtikalId { get; set; }
        public string KorisnickoIme { get; set; }
    }
}
