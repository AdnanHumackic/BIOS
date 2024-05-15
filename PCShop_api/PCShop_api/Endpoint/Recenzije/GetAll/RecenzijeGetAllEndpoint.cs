using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Helper;

namespace PCShop_api.Endpoint.Recenzije.GetAll
{
    [Route("Recenzije-GetAll")]
    public class RecenzijeGetAllEndpoint : MyBaseEndpoint<RecenzijeGetAllRequest,RecenzijeGetAllResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public RecenzijeGetAllEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet]
        public override async Task<RecenzijeGetAllResponse> Akcija([FromQuery]RecenzijeGetAllRequest request, CancellationToken cancellationToken)
        {
            var noviObj = await _applicationDbContext.Recenzije
                .Select(x => new RecenzijeGetAllResponseRecenzije()
                {
                    ID = x.ID,
                    DatumDodavanja = x.DatumDodavanja,
                    Sadrzaj = x.Sadrzaj,
                    ArtikalId = x.ArtikalId,
                    KorisnickoIme = x.EvidentiraoKorisnik.KorisnickoIme
                }).ToListAsync();

            return new RecenzijeGetAllResponse
            {
                Recenzije = noviObj
            };
        }
    }
}
