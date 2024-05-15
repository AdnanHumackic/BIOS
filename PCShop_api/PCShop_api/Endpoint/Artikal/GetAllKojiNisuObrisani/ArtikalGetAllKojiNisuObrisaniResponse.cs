namespace PCShop_api.Endpoint.Artikal.GetAllKojiNisuObrisani
{
    public class ArtikalGetAllKojiNisuObrisaniResponse
    {
        public List<ArtikalGetAllKojiNisuObrisaniResponseArtikal> Artikli { get; set; }
    }
    public class ArtikalGetAllKojiNisuObrisaniResponseArtikal
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
