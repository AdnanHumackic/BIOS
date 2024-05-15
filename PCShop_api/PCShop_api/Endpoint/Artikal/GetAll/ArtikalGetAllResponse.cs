namespace PCShop_api.Endpoint.Artikal.GetAll
{
    public class ArtikalGetAllResponse
    {
        public List<ArtikalGetAllResponseArtikal> Artikli { get; set; }
    }

    public class ArtikalGetAllResponseArtikal
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public int Cijena { get; set; }
        public string Proizvodjac { get; set; }
        public string Tip { get; set; }
        public string Opis { get; set; }
        //public string Slika { get; set; }
        public bool isObrisan { get; set; }


    }
}
