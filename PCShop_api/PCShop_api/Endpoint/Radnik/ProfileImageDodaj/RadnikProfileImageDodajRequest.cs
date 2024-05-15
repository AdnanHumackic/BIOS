namespace PCShop_api.Endpoint.Radnik.ProfileImageDodaj
{
    public class RadnikProfileImageDodajRequest
    {
        public int RadnikID { get; set; }
        public IFormFile SlikaRadka { set; get; }
    }
}
