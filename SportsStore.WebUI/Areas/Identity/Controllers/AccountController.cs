using System.Threading.Tasks;
using SportsStore.WebUI.Models;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Areas.Identity.Models;
using SportsStore.Domain.Entities.IdentityEntities;

namespace SportsStore.WebUI.Areas.Identity.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class AccountController : Controller
    {
        private AppUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<AppUserManager>(); }
        }

        private IAuthenticationManager AuthManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        [AllowAnonymous]
        public PartialViewResult LoginSummary()
        {
            return PartialView();
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return View("AuthorizationError", new string[] { $"Access Denied. You are logged in as {HttpContext.User.Identity.Name}" });
            }
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel details, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await UserManager.FindAsync(details.Name, details.Password);
                if (user == null)
                {
                    TempData["message"] = "Invalid name or password";
                }
                else
                {
                    ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = false
                    }, ident);
                    return Redirect(returnUrl);
                }
            }
            return View(details);
        }
        [AllowAnonymous]
        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return RedirectToAction("List", "Product", new { area = "" });
        }
    }
}