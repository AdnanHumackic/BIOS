using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Data.Models;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Artikal.GetAllKojiNisuObrisani
{
    [Route("Artikal-GetAllKojiNisuObrisani")]
    //[Autorizacija(radnik: true, admin: false, kupac: false)]
    [MyAuthorization]
    public class ArtikalGetAllKojiNisuObrisaniEndpoint : MyBaseEndpoint<ArtikalGetAllKojiNisuObrisaniRequest, ArtikalGetAllKojiNisuObrisaniResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _authService;

        public ArtikalGetAllKojiNisuObrisaniEndpoint(ApplicationDbContext applicationDbContext, MyAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
        }

        [HttpGet]
        public override async Task<ArtikalGetAllKojiNisuObrisaniResponse> Akcija([FromQuery] ArtikalGetAllKojiNisuObrisaniRequest request, CancellationToken cancellationToken)
        {
            var art = await _applicationDbContext.Artikal.Where(x => x.isObrisan == false)
                .Select(x => new ArtikalGetAllKojiNisuObrisaniResponseArtikal()
                {
                    ID = x.ID,
                    Naziv = x.ImeArtikla,
                    Cijena = x.Cijena,
                    Proizvodjac = x.Proizvodjac,
                    Tip = x.TipArtikla.Tip,
                    Opis = x.Opis,
                    //Slika = x.Slika,
                    isObrisan = x.isObrisan
                })
                .ToListAsync(cancellationToken:cancellationToken);

            return new ArtikalGetAllKojiNisuObrisaniResponse
            {
                Artikli = art
            };
        }
    }
}
