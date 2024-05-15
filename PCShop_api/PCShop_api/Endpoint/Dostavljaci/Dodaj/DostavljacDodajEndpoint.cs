using Microsoft.AspNetCore.Mvc;
using PCShop_api.Data;
using PCShop_api.Helper;


namespace PCShop_api.Endpoint.Dostavljaci.Dodaj
{
    [Route("Dostavljac-Dodaj")]
    public class DostavljacDodajEndpoint:MyBaseEndpoint<DostavljacDodajRequest, DostavljacDodajResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public DostavljacDodajEndpoint(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            
        }

        [HttpPost]
        public override async Task<DostavljacDodajResponse> Akcija([FromBody]DostavljacDodajRequest request, CancellationToken cancellationToken)
        {
            var noviDostavljac = new Data.Models.Dostavljaci
            {
                ID = request.ID,
                Naziv = request.Naziv,
                CijenaDostave = request.CijenaDostave,
                Sjediste = request.Sjediste
            };
            _applicationDbContext.Dostavljaci.Add(noviDostavljac);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new DostavljacDodajResponse
            {

            };
        }
    }
}
