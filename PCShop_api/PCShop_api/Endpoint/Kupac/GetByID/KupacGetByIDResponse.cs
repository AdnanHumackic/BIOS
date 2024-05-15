using System.ComponentModel.DataAnnotations.Schema;

namespace PCShop_api.Endpoint.Kupac.GetByID
{
    public class KupacGetByIDResponse
    {
        public int ID { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string? Email { get; set; }

        public int Drzava { get; set; }
        public string SlikaKorisnika { get; set; }

    }
}
