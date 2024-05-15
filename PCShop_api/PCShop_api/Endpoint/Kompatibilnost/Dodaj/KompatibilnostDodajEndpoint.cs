using Microsoft.AspNetCore.Mvc;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Kompatibilnost.Dodaj
{
    [MyAuthorization]
    [Route("Kompatibilnost-Dodaj")]
    public class KompatibilnostDodajEndpoint : MyBaseEndpoint<KompatibilnostDodajRequest, KompatibilnostDodajResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public KompatibilnostDodajEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public override async Task<KompatibilnostDodajResponse> Akcija([FromBody] KompatibilnostDodajRequest request, CancellationToken cancellationToken)
        {
            //upod
            var kompObj = new Data.Models.Kompatibilnost()
            {
                Artikal1ID = request.Artikal1,
                Artikal2ID = request.Artikal2
            };
            if (kompObj == null)
            {
                throw new Exception("Odaberite artikle");
            }
            _applicationDbContext.Add(kompObj);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new KompatibilnostDodajResponse
            {

            };


        }
    }
}
