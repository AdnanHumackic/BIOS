using PCShop_api.Helper;

namespace PCShop_api.Endpoint.Artikal.GetAllKojiNisuObrisaniPaged
{
    public class ArtikalGetAllKojiNisuObrisaniPagedResponse
    {
        public PagedList<ArtikalGetAllKojiNisuObrisaniPagedResponseArtikal> Artikli { get; set; }
    }
    public class ArtikalGetAllKojiNisuObrisaniPagedResponseArtikal
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public int Cijena { get; set; }
        public string Proizvodjac { get; set; }
        public string Tip { get; set; }
        public string Opis { get; set; }
        public bool isObrisan { get; set; }
    }
}
