namespace PCShop_api.Endpoint.Kompatibilnost.GetByID
{
    public class KompatibilnostGetByIDResponse
    {
        public List<KompatibilnostGetByIDResponseKompatibilnost> Komp { get; set; }

    }
    public class KompatibilnostGetByIDResponseKompatibilnost
    {
        public int ID { get; set; }
        public int KompatibilnostID { get; set; }

        public string Artikal2Ime { get; set; }
        public int Cijena { get; set; }

    }
}
