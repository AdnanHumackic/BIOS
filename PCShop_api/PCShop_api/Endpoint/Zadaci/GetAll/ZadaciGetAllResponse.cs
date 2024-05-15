using PCShop_api.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCShop_api.Endpoint.Zadaci.GetAll
{
    public class ZadaciGetAllResponse
    {
        public List<ZadaciGetAllResponseZadaci> StavkeZadatak { get; set; }
    }
        public class ZadaciGetAllResponseZadaci
        {
            public int Id { get; set; }
            public string Naziv { get; set; }
            public string Opis { get; set; }
            public DateTime DatumDodavanja { get; set; } = DateTime.Now;
            public DateTime DatumZavrsetka { get; set; }
        }
}
