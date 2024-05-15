using Microsoft.AspNetCore.Mvc;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Recenzije.RecenzijeGetByIDPaged
{
    //[MyAuthorization]
    [Route("Recenzija-GetByIDPaged")]
    public class RecenzijeGetByIDPagedEndpoint:MyBaseEndpoint<RecenzijeGetByIDPagedRequest,RecenzijeGetByIDPagedResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RecenzijeGetByIDPagedEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public override async Task<RecenzijeGetByIDPagedResponse> Akcija([FromQuery]RecenzijeGetByIDPagedRequest request, CancellationToken cancellationToken)
        {
            var recenzija = _applicationDbContext.Recenzije.Where(x => x.ArtikalId == request.ID).Select(x => new RecenzijeGetByIDPagedResponseRecenzije()
            {
                ArtikalId = x.ArtikalId,
                DatumDodavanja = x.DatumDodavanja,
                Sadrzaj = x.Sadrzaj,
                EvidentiraoKorisnikId = x.EvidentiraoKorisnikId,
                KorisnickoIme = x.EvidentiraoKorisnik.KorisnickoIme
            });

            var dataOfOnePage = PagedList<RecenzijeGetByIDPagedResponseRecenzije>.Create(recenzija, request.PageNumber, request.PageSize);
            return new RecenzijeGetByIDPagedResponse
            {
                Recenzije = dataOfOnePage
            };
        }
    }
}
