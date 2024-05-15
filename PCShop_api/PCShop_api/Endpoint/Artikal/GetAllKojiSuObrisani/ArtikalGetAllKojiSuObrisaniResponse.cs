namespace PCShop_api.Endpoint.Artikal.GetAllKojiSuObrisani
{
    public class ArtikalGetAllKojiSuObrisaniResponse
    {
        public List<ArtikalGetAllKojiSuObrisaniResponseArtikal> ObrisaniArt { get; set; }
    }
    public class ArtikalGetAllKojiSuObrisaniResponseArtikal
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
