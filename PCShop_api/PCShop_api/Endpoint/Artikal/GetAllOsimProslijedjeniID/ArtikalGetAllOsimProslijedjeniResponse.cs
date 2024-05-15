namespace PCShop_api.Endpoint.Artikal.GetAllOsimProslijedjeniID
{
    public class ArtikalGetAllOsimProslijedjeniResponse
    {
        public List<ArtikalGetAllOsimProslijedjeniResponseArtikal> Artikal { get; set; }
    }
    public class ArtikalGetAllOsimProslijedjeniResponseArtikal
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
    }
}
