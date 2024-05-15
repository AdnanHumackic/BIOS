using PCShop_api.Helper;

namespace PCShop_api.Endpoint.Artikal.PretragaPaged
{
    public class ArtikalPretragaPagedResponse
    {
        public PagedList<ArtikalPretragaPagedResponseArtikal> Artikal { get; set; }
    }
    public class ArtikalPretragaPagedResponseArtikal
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public int Cijena { get; set; }
        public string Proizvodjac { get; set; }
        public string Tip { get; set; }
        public string Opis { get; set; }
    }
}
