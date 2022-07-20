using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Polls.Domain.Const;
using Polls.Domain.Interfaces.IServices;
using Polls.Domain.ViewModel.Account;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Polls.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly IToastNotification _toastNotification;

        public AccountController(IUserServices userServices, IToastNotification toastNotification)
        {
            _userServices = userServices;
            _toastNotification = toastNotification;
        }

        public IActionResult Login(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            ViewBag.url = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage("model state is not valid");
                return View();
            }

            var Result = _userServices.Login(login.UserId, login.Password);

            if (Result == null)
                return View();

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, Result, new AuthenticationProperties()
            {
                IsPersistent = login.RememberMe
            });

            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");        
        }
    }
}
