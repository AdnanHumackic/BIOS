using System.ComponentModel.DataAnnotations.Schema;

namespace PCShop_api.Endpoint.Radnik.GetAll
{
    public class RadnikGetAllResponse
    {
        public List<RadnikGetAllResponseRadnik> Radnik { get; set; }
    }
    public class RadnikGetAllResponseRadnik
    {
        public int ID { get; set; }
        public string KorisnickoIme { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public DateTime DatumRodjenja { get; set; }

        public DateTime DatumZaposlenja { get; set; }
    }
}
