namespace PCShop_api.Endpoint.Kupac.ProfileImageDodaj
{
    public class KupacProfileImageDodajRequest
    {
        public int KupacID { get; set; }
        public IFormFile SlikaKupca { set; get; }
    }
}
