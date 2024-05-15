using PCShop_api.Helper;

namespace PCShop_api.Endpoint.Zadaci.GetByIDPaged
{
    public class ZadaciGetByIDPagedResponse
    {
        public PagedList<ZadaciGetByIDPagedResponseZadaci> Zadaci { get; set; }
    }
    public class ZadaciGetByIDPagedResponseZadaci
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public DateTime DatumDodavanja { get; set; } = DateTime.Now;
        public DateTime DatumZavrsetka { get; set; }
    }
}
