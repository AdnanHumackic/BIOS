namespace PCShop_api.Endpoint.ArtikalSlika.Update
{
    public class ArtikalSlikaUpdateRequest
    {
        public int IDArtikla { get; set; }
        public List<string>? SlikaArtikla { get; set; }
    }
}
