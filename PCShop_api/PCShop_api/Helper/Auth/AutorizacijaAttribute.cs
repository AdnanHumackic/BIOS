using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using PCShop_api.Data.Models;
using PCShop_api.Data;

namespace PCShop_api.Helper.Auth
{

    public class MyAuthorizationAttribute : TypeFilterAttribute
    {
        public MyAuthorizationAttribute() : base(typeof(MyAuthorizationAsyncActionFilter))
        {
        }
    }
    public class MyAuthorizationAsyncActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var authService = context.HttpContext.RequestServices.GetService<MyAuthService>()!;

            if (!authService.JelLogiran())
            {
                context.Result = new UnauthorizedObjectResult("niste logirani na sistem");
                return;
            }
            



            await next();
        }
    }
    //dole je ok kod

    //public class AutorizacijaAttribute : TypeFilterAttribute
    //{
    //    public AutorizacijaAttribute(bool radnik, bool admin, bool kupac)
    //        : base(typeof(MyAuthorizeImpl))
    //    {
    //        Arguments = new object[] { radnik, admin, kupac};
    //    }
    //}


    //public class MyAuthorizeImpl : IAsyncActionFilter
    //{
    //    private readonly bool _radnik;
    //    private readonly bool _admin;
    //    private readonly bool _kupac;

    //    public MyAuthorizeImpl(bool radnik, bool admin, bool kupac)
    //    {
    //        _radnik = radnik;
    //        _admin = admin;
    //        _kupac = kupac;
    //    }

    //    public async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
    //    {
    //        var dbContext = filterContext.HttpContext.RequestServices.GetService<ApplicationDbContext>()!;
    //        var authService = filterContext.HttpContext.RequestServices.GetService<MyAuthService>()!;

    //        var loginInfo = authService.GetAuthInfo();

    //        if (loginInfo == null || !loginInfo.IsLogiran)
    //        {
    //            filterContext.Result = new UnauthorizedResult();
    //            return;
    //        }

    //        KorisnickiNalog k = loginInfo.KorisnickiNalog;

    //        bool imaPravoPristupa = k.isKupac && _kupac;

    //        if (k.isRadnik && _radnik)
    //        {
    //            imaPravoPristupa = true;
    //        }

    //        if (k.isAdmin && _admin)
    //        {
    //            imaPravoPristupa = true;
    //        }
    //        if (k.isKupac && _kupac)
    //        {
    //            imaPravoPristupa = true;
    //        }


    //        if (imaPravoPristupa)
    //        {
    //            await next();
    //        }
    //        else
    //        {
    //            filterContext.Result = new UnauthorizedResult();
    //        }
    //    }
    //}
}
