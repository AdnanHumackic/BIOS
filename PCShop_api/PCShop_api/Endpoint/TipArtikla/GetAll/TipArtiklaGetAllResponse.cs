namespace PCShop_api.Endpoint.TipArtikla.GetAll
{
    public class TipArtiklaGetAllResponse
    {
        public List<TipArtiklaGetAllResponseTip> Tip { get; set; }
    }
    public class TipArtiklaGetAllResponseTip
    {
        public int ID { get; set; }
        public string TipArtikla { get; set; }
    }
}
