using PCShop_api.Helper;

namespace PCShop_api.Endpoint.Narudzba.GetByIDPaged
{
    public class NarudzbaGetByIDPagedResponse
    {
        public PagedList<NarudzbaGetByIDPagedResponseNarudzba> Narudzba { get; set; }
    }
    public class NarudzbaGetByIDPagedResponseNarudzba
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
