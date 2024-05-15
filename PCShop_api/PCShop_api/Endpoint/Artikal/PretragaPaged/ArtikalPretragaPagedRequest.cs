namespace PCShop_api.Endpoint.Artikal.PretragaPaged
{
    public class ArtikalPretragaPagedRequest
    {
        public string? Pretraga { get; set; }
        public string? Proizvodjac { get; set; }

        public int? TipID { get; set; }
        public int? cijenaOd { get; set; }
        public int? cijenaDo { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
