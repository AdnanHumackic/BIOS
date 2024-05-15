using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Data.Models;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;
using static PCShop_api.Endpoint.Artikal.GetAll.ArtikalGetAllResponse;

namespace PCShop_api.Endpoint.Artikal.GetAll
{
    [Route("Artikal-GetAll")]
    public class ArtikalGetAllEndpoint : MyBaseEndpoint<ArtikalGetAllRequest, ArtikalGetAllResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ArtikalGetAllEndpoint(ApplicationDbContext applicationDbContext, MyAuthService authService)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public override async Task<ArtikalGetAllResponse> Akcija([FromQuery] ArtikalGetAllRequest request, CancellationToken cancellationToken)
        {
            var artikalObj = await _applicationDbContext.Artikal.Where(x => x.isObrisan == false)
                .Select(x => new ArtikalGetAllResponseArtikal()
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

            return new ArtikalGetAllResponse
            {
                Artikli = artikalObj
            };
        }
    }
}
