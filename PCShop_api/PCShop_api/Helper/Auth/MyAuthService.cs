using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Data.Models;
using System.Text.Json.Serialization;

namespace PCShop_api.Helper.Auth
{
    public class MyAuthService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MyAuthService(ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _applicationDbContext = applicationDbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public bool JelLogiran()
        {
            return GetAuthInfo().isLogiran;
        }

        public bool IsAdmin()
        {
            return GetAuthInfo().korisnickiNalog?.isAdmin ?? false;
        }
        public bool IsRadnik()
        {
            return GetAuthInfo().korisnickiNalog?.isRadnik ?? false;
        }
        public bool IsKupac()
        {
            return GetAuthInfo().korisnickiNalog?.isKupac ?? false;
        }
        public MyAuthInfo GetAuthInfo()
        {
            string? authToken = _httpContextAccessor.HttpContext!.Request.Headers["my-auth-token"];


            AutentifikacijaToken? autentifikacijaToken = _applicationDbContext.AutentifikacijaToken
                .Include(x => x.korisnickiNalog)
                .SingleOrDefault(x => x.Vrijednost == authToken);

            return new MyAuthInfo(autentifikacijaToken);
        }
    }

    public class MyAuthInfo
    {
        public MyAuthInfo(AutentifikacijaToken? autentifikacijaToken)
        {
            this.autentifikacijaToken = autentifikacijaToken;
        }

        [JsonIgnore]
        public KorisnickiNalog? korisnickiNalog => autentifikacijaToken?.korisnickiNalog;
        public AutentifikacijaToken? autentifikacijaToken { get; set; }

        public bool isLogiran => korisnickiNalog != null;

    }
}
