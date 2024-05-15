using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Recenzije.GetById
{
    [Route("Recenzije-GetById")]
    public class RecenzijaGetbyIdEndpoint:MyBaseEndpoint<RecenzijaGetbyIdRequest,RecenzijaGetbyIdResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;
        public RecenzijaGetbyIdEndpoint(ApplicationDbContext applicationDbContext, MyAuthService myAuthService)
        {
            _applicationDbContext = applicationDbContext;
            _myAuthService = myAuthService;
        }
        [HttpGet]
        public override async Task<RecenzijaGetbyIdResponse> Akcija([FromQuery]RecenzijaGetbyIdRequest request, CancellationToken cancellationToken)
        {
            var recenzija = await _applicationDbContext.Recenzije.Where(x => x.ArtikalId == request.ID).Select(x => new RecenzijaGetbyIdResponseRecenzija()
            {
                ArtikalId = x.ArtikalId,
                DatumDodavanja = x.DatumDodavanja,
                Sadrzaj = x.Sadrzaj,
                EvidentiraoKorisnikId = x.EvidentiraoKorisnikId,
                KorisnickoIme=x.EvidentiraoKorisnik.KorisnickoIme
            }).ToListAsync(cancellationToken);

            return new RecenzijaGetbyIdResponse
            {
                Recenzija = recenzija
            };
        }
    }
}
