using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Data.Models;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Artikal.GetAllKojiSuObrisani
{
    [Route("Artikal-GetAllKojiSuObrisani")]
    //[Autorizacija(radnik: true, admin: false, kupac: false)]
    [MyAuthorization]

    public class ArtikalGetAllKojiSuObrisaniEndpoint : MyBaseEndpoint<ArtikalGetAllKojiSuObrisaniRequest, ArtikalGetAllKojiSuObrisaniResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _authService;
        public ArtikalGetAllKojiSuObrisaniEndpoint(ApplicationDbContext applicationDbContext, MyAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
            _authService = authService;
        }

        [HttpGet]
        public override async Task<ArtikalGetAllKojiSuObrisaniResponse> Akcija([FromQuery] ArtikalGetAllKojiSuObrisaniRequest request, CancellationToken cancellationToken)
        {
            var obrArt = await _applicationDbContext.Artikal.Where(x => x.isObrisan == true)
                .Select(x => new ArtikalGetAllKojiSuObrisaniResponseArtikal()
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

            return new ArtikalGetAllKojiSuObrisaniResponse
            {
                ObrisaniArt = obrArt
            };
        }
    }
}
