namespace PCShop_api.Endpoint.ArtikalSlika.Dodaj
{
    public class ArtikalSlikaDodajRequest
    {
        public int ArtikalID { get; set; }
        public IFormFile SlikaArtikla { set; get; }
    }
}
