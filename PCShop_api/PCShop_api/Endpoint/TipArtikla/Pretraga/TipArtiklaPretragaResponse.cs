namespace PCShop_api.Endpoint.TipArtikla.Pretraga
{
    public class TipArtiklaPretragaResponse
    {
        public List<TipArtiklaPretragaResponseTipArtikla> TipArtikala { get; set; }
    }
    public class TipArtiklaPretragaResponseTipArtikla
    {
        public int ID { get; set; }
        public string Tip { get; set; }
    }
}
