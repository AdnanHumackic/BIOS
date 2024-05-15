namespace PCShop_api.Endpoint.Wishlist.GetByIDPaged
{
    public class WishlistGetByIDPagedRequest
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int ID { get; set; }

    }
}
