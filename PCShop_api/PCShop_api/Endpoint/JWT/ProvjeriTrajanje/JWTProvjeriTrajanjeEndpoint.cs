using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCShop_api.Data;
using PCShop_api.Data.Models;
using PCShop_api.Helper;
using PCShop_api.Helper.Auth;

namespace PCShop_api.Endpoint.JWT.ProvjeriTrajanje
{

    [MyAuthorization]
    [Route("JWT")]
    [ApiController]
    public class JWTProvjeriTrajanjeEndpoint : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuth;

        public JWTProvjeriTrajanjeEndpoint(ApplicationDbContext applicationDbContext, MyAuthService myAuth)
        {
            _applicationDbContext = applicationDbContext;
            _myAuth = myAuth;
        }
        [HttpPost("Provjeri")]
        public async Task<ActionResult<JWTProvjeriTrajanjeResponse>> ProvjeriTrajanjeTokena([FromHeader(Name = "my-auth-token")] string oldToken, CancellationToken cancellationToken)
        {
            var trenutnoPrijavljenToken =await  _applicationDbContext.AutentifikacijaToken
                .Where(x => x.Vrijednost == oldToken)
                .Include(x=>x.korisnickiNalog)
                .FirstOrDefaultAsync();
            if (trenutnoPrijavljenToken == null)
            {
                return Ok(new JWTProvjeriTrajanjeResponse { Poruka = "Niste prijavljeni!" });
            }
            else
            {
                TimeSpan trajanje = TimeSpan.FromMinutes(90);

                DateTime vrijemeIsteka = trenutnoPrijavljenToken.vrijemeEvidentiranja.Add(trajanje);

                if (DateTime.Now > vrijemeIsteka)
                {
                    string noviToken = TokenGenerator.Generate();

                    _applicationDbContext.Remove(trenutnoPrijavljenToken);

                    trenutnoPrijavljenToken.Vrijednost = noviToken;
                    trenutnoPrijavljenToken.vrijemeEvidentiranja = DateTime.Now;

                    _applicationDbContext.AutentifikacijaToken.Update(trenutnoPrijavljenToken);
                    await _applicationDbContext.SaveChangesAsync(cancellationToken);
                        
                        
                    return Ok(new JWTProvjeriTrajanjeResponse
                    {
                        Poruka="Generisan novi token!",
                        Istekao = true,
                        NoviToken = trenutnoPrijavljenToken.Vrijednost,
                        autentifikacijaToken = trenutnoPrijavljenToken,
                    });

                }
                else
                {
                    return Ok(new JWTProvjeriTrajanjeResponse 
                    {
                        Poruka = "Token nije istekao!",
                        Istekao = false,
                        NoviToken=trenutnoPrijavljenToken.Vrijednost,
                        autentifikacijaToken=trenutnoPrijavljenToken
                    });
                }
            }
            return Ok();
        }
    }
}
