namespace PCShop_api.Endpoint.Dostavljaci.GetAll
{
    public class DostavljacGetAllResponse
    {
        public List<DostavljacGetAllResponseDostavljac> Dostavljac { get; set; }
    }
    public class DostavljacGetAllResponseDostavljac
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public float CijenaDostave { get; set; }
        public string Sjediste { get; set; }
    }
}
