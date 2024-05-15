namespace PCShop_api.Endpoint.Zadaci.GetByIDPaged
{
    public class ZadaciGetByIDPagedRequest
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int ID { get; set; }
    }
}
