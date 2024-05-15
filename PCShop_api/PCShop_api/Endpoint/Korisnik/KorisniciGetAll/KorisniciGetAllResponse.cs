namespace PCShop_api.Endpoint.Korisnik.KorisniciGetAll
{
    public class KorisniciGetAllResponse
    {
        public List<KorisniciGetAllResponseKorisnici> Korisnici { get;set; }
    }
    public class KorisniciGetAllResponseKorisnici
    {
        public int ID { get; set; }
        public string KorisnickoIme { get; set; }
        public string? SlikaKorisnika { get; set; }
        public bool isKupac { get; set; }
        public bool isRadnik { get; set; }
        public bool isAdmin { get; set; }
    }
}
