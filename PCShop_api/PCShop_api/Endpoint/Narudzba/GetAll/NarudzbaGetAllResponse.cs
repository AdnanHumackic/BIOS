namespace PCShop_api.Endpoint.Narudzba.GetAll
{
    public class NarudzbaGetAllResponse
    {
        public List<NarudzbaGetAllResponseNarudzba> Narudzbe { get; set; }
    }

    public class NarudzbaGetAllResponseNarudzba
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
