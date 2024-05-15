namespace PCShop_api.Endpoint.Radnik.LozinkaUpdate
{
    public class RadnikLozinkaUpdateRequest
    {
        public int ID { get; set; }
        public string Lozinka { get; set; }
        public string? SlikaRadnika { get; set; }
    }
}
