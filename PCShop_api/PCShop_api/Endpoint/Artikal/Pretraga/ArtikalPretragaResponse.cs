namespace PCShop_api.Endpoint.Artikal.Pretraga
{
    public class ArtikalPretragaResponse
    {
        public List<ArtikalPretragaResponseArtikal> Artikli { get; set; }
    }
    public class ArtikalPretragaResponseArtikal
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public int Cijena { get; set; }
        public string Proizvodjac { get; set; }
        public string Tip { get; set; }
        //public string Slika { get; set; }
        public string Opis { get; set; }


    }
}
