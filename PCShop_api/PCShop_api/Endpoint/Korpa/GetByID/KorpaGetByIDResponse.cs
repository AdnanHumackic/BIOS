namespace PCShop_api.Endpoint.Korpa.GetByID
{
    public class KorpaGetByIDResponse
    {
        public List<KorpaGetByIDResponseKorpa> Korpa { get; set; }
    }
    public class KorpaGetByIDResponseKorpa
    {
        public int ID { get; set; }
        public int ArtikalID { get; set; }
        public string ImeArtikla { get; set; }
        public int Cijena { get; set; }
        public string Opis { get; set; }
        //public string Slika { get; set; }
        public DateTime DatumDodavanja { get; set; }
    }
}
