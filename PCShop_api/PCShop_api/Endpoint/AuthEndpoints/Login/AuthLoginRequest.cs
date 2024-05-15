namespace PCShop_api.Endpoint.AuthEndpoints.Login
{
    public class AuthLoginRequest
    {
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public string SignalRConnectionID { get; set; }

    }
}
