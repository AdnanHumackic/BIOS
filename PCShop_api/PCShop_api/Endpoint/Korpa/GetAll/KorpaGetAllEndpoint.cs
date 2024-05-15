
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;
using static PCShop_api.Endpoint.Korpa.GetAll.KorpaGetAllResponse;

namespace PCShop_api.Endpoint.Korpa.GetAll
{
    [MyAuthorization]
    [Route("Korpa-GetAll")]
    public class KorpaGetAllEndpoint : MyBaseEndpoint<KorpaGetAllRequest,KorpaGetAllResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public KorpaGetAllEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public override async Task<KorpaGetAllResponse> Akcija([FromQuery] KorpaGetAllRequest request, CancellationToken cancellationToken)
        {
            var korpaLista = await _applicationDbContext.Korpa
                .Select(x => new KorpaGetAllResponseKorpa()
                {
                    ID= x.ID,
                    ArtikalID = x.ArtikalID,
                    ImeArtikla = x.Artikal.ImeArtikla,
                    Cijena = x.Artikal.Cijena,
                    Opis = x.Artikal.Opis,
                    //Slika = x.Artikal.Slika
                }).ToListAsync(cancellationToken);

            return new KorpaGetAllResponse
            {
                StavkeKorpa = korpaLista
            };
        }
    }
}
