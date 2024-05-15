using Microsoft.AspNetCore.Mvc;
using PCShop_api.Data;
using PCShop_api.Data.Models;
using PCShop_api.Endpoint.Narudzba.GetAll;
using PCShop_api.Endpoint.Narudzba.GetByIDPaged;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.Narudzba.GetAllPaged
{
    [MyAuthorization]
    [Route("Narudzba-GetAllPaged")]
    public class NarudzbaGetAllPagedEndpoint:MyBaseEndpoint<NarudzbaGetAllPagedRequest, NarudzbaGetAllPagedResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public NarudzbaGetAllPagedEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public override async Task<NarudzbaGetAllPagedResponse> Akcija([FromQuery]NarudzbaGetAllPagedRequest request, CancellationToken cancellationToken)
        {
            var narudzbaObj = _applicationDbContext.Narudzba
                .Select(x => new NarudzbaGetAllPagedResponseNarudzba()
                {
                    ID = x.ID,
                    Ime = x.Ime,
                    Prezime = x.Prezime,
                    Adresa = x.Adresa,
                    BrojTelefona = x.BrojTelefona,
                    Dostavljac = x.Dostavljac,
                    UkupnaCijena = x.UkupnaCijena
                });

            var dataOfOnePage = PagedList<NarudzbaGetAllPagedResponseNarudzba>.Create(narudzbaObj, request.PageNumber, request.PageSize);

            return new NarudzbaGetAllPagedResponse
            {
                Narudzbe = dataOfOnePage
            };
        }
    }
}
