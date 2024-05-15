using PCShop_api.Helper;

namespace PCShop_api.Endpoint.Narudzba.GetAllPaged
{
    public class NarudzbaGetAllPagedResponse
    {
        public PagedList<NarudzbaGetAllPagedResponseNarudzba> Narudzbe { get; set; }
    }
    public class NarudzbaGetAllPagedResponseNarudzba
    {
        public int ID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Adresa { get; set; }
        public string BrojTelefona { get; set; }
        public string Dostavljac { get; set; }
        public float UkupnaCijena { get; set; }
    }
}
