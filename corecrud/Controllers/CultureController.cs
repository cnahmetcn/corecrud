using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace corecrud.Controllers
{
    public class CultureController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CultureManagement(string culture, string returnUrl)
        {
            string url = returnUrl;
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(1) });
            // return RedirectToAction(nameof(Index));
            if (returnUrl != "~/")
            {
                url = returnUrl.Replace(returnUrl.Substring(2, 2), culture);
            }
            return LocalRedirect(url);
        }
    }
}
