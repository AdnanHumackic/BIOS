using PCShop_api.Endpoint.Artikal.GetAllKojiNisuObrisaniPaged;
using PCShop_api.Helper;

namespace PCShop_api.Endpoint.Artikal.GetAllKojiSuObrisaniPaged
{
    public class ArtikalGetAllKojiSuObrisaniPagedResponse
    {
        public PagedList<ArtikalGetAllKojiSuObrisaniPagedResponseArtikal> Artikli { get; set; }

    }
    public class ArtikalGetAllKojiSuObrisaniPagedResponseArtikal
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
