namespace PCShop_api.Endpoint.Recenzije.RecenzijeDodaj
{
    public class RecenzijeDodajRequest
    {
        public int ID { get; set; }
        public string Sadrzaj { get; set; }
        public DateTime DatumDodavanja { get; set; }
        public int EvidentiraoKorisnikId { get; set; }
        public int ArtikalId { get; set; }
    }
}
