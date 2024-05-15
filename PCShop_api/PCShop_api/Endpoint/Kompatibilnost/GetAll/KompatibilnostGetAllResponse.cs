namespace PCShop_api.Endpoint.Kompatibilnost.GetAll
{
    public class KompatibilnostGetAllResponse
    {
        public List<KompatibilnostGetAllResponseKompatibilnost> Kompatibilnost { get; set; }
    }

    public class KompatibilnostGetAllResponseKompatibilnost
    {
        public int ID { get; set; }
        public string Artikal1 { get; set; }
        public string Artikal2 { get; set; }
        public int Art1ID { get; set; }
        public int Art2ID { get; set; }
    }
}
