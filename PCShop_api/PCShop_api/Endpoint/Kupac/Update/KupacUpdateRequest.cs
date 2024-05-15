namespace PCShop_api.Endpoint.Kupac.Update
{
    public class KupacUpdateRequest
    {
        public int ID { get; set; }
        public string KorisnickoIme { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }

        public int Drzava { get; set; }
        public string? SlikaKorisnika { get; set; }

    }
}
