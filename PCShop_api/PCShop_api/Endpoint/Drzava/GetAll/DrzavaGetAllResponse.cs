namespace PCShop_api.Endpoint.Drzava.GetAll
{
    public class DrzavaGetAllResponse
    {
        public List<DrzavaGetAllResponseDrzava> Drzava { get; set; }
    }
    public class DrzavaGetAllResponseDrzava
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public string? Skracenica { get; set; }
    }
}
