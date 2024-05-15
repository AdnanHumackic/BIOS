namespace PCShop_api.Endpoint.Narudzba.GetByIDPaged
{
    public class NarudzbaGetByIDPagedRequest
    {
        public int ID { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

    }
}
