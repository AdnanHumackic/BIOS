namespace PCShop_api.Endpoint.Artikal.Update
{
    public class ArtikalUpdateRequest
    {
        public int ID { get; set; }
        public string ImeArtikla { get; set; }
        public string Proizvodjac { get; set; }
        public int Cijena { get; set; }
        public string Opis { get; set; }
        //public string Slika { get; set; }
        public int TipID { get; set; }
    }
}
