namespace PCShop_api.Endpoint.KorisnickiNalog.UpdateZaAdmina
{
    public class AdminUpdateRequest
    {
        public int ID { get; set; }
        public bool isAdmin { get; set; }
        public bool isKupac { get; set; }
        public bool isRadnik { get; set; }
    }
}
