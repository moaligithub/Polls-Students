using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polls.Domain.Const;
using Polls.Domain.Interfaces.IServices;
using Polls.Domain.ViewModel.Home;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Polls.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeServices _homeServices;

        public HomeController(IHomeServices homeServices)
        {
            _homeServices = homeServices;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated && User.FindFirst(ClaimTypes.Role).Value == Roles.Admin)
                return RedirectToAction(nameof(IndexDashboard));

            return View(await _homeServices.GetHomeIndexAsync(4, 6));
        }

        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> IndexDashboard()
        {
            return View(await _homeServices.GetDashboardHomeAsync());
        }
    }
}