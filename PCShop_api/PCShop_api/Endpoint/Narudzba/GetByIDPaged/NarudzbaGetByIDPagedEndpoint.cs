using Microsoft.AspNetCore.Mvc;
using PCShop_api.Data;
using PCShop_api.Data.Models;
using PCShop_api.Endpoint.Narudzba.GetByID;
using PCShop_api.Endpoint.Wishlist.GetByIDPaged;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Narudzba.GetByIDPaged
{
    [MyAuthorization]
    [Route("Narudzba-GetByIDPaged")]
    public class NarudzbaGetByIDPagedEndpoint:MyBaseEndpoint<NarudzbaGetByIDPagedRequest, NarudzbaGetByIDPagedResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        
        public NarudzbaGetByIDPagedEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public override async Task<NarudzbaGetByIDPagedResponse> Akcija([FromQuery]NarudzbaGetByIDPagedRequest request, CancellationToken cancellationToken)
        {
            var narudzba = _applicationDbContext.Narudzba.Where(x => x.EvidentiraoKorisnikId == request.ID)
                .Select(x => new NarudzbaGetByIDPagedResponseNarudzba()
                {
                    ID = x.ID,
                    Adresa = x.Adresa,
                    BrojTelefona = x.BrojTelefona,
                    Dostavljac = x.Dostavljac,
                    Ime = x.Ime,
                    Prezime = x.Prezime,
                    UkupnaCijena = x.UkupnaCijena
                });

            var dataOfOnePage = PagedList<NarudzbaGetByIDPagedResponseNarudzba>.Create(narudzba, request.PageNumber, request.PageSize);

            return new NarudzbaGetByIDPagedResponse
            {
                Narudzba = dataOfOnePage
            };
        }
    }
}
