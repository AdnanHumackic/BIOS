namespace PCShop_api.Endpoint.Narudzba.GetByID
{
    public class NarudzbaGetByIDResponse
    {
        public List<NarudzbaGetByIdReponseNarudzba> Narudzba { get; set; }
    }
    public class NarudzbaGetByIdReponseNarudzba
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
