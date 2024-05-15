namespace PCShop_api.Endpoint.KorisnickiNalog.ProfileImageDodaj
{
    public class KorisnickiNalogProfileImageDodajRequest
    {
        public int IDKorisnika { get; set; }
        public IFormFile SlikaKorisnika { set; get; }
    }
}
