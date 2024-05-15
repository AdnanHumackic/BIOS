using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.JWT.ProvjeriTrajanje
{
    public class JWTProvjeriTrajanjeResponse
    {
        public bool Istekao { get; set; }
        public string NoviToken { get; set; }
        public string? Poruka { get; set; }
        public Data.Models.AutentifikacijaToken autentifikacijaToken { get; set; }
        
    }
}
