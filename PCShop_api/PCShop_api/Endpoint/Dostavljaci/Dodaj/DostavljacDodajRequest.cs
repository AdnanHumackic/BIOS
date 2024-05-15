namespace PCShop_api.Endpoint.Dostavljaci.Dodaj
{
    public class DostavljacDodajRequest
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public float CijenaDostave { get; set; }
        public string Sjediste { get; set; }
    }
}
