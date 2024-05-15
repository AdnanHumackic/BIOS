namespace PCShop_api.Endpoint.Admin.Update
{
    public class AdminUpdatePodaciRequest
    {
        public int ID { get; set; }
        public string KorisnickoIme { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int Drzava { get; set; }
        public string? SlikaKorisnika { get; set; }
    }
}
