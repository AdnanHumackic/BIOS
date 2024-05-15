namespace PCShop_api.Endpoint.Recenzije.RecenzijeGetByIDPaged
{
    public class RecenzijeGetByIDPagedRequest
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int ID { get; set; }
    }
}
