using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Polls.Domain.Const;
using Polls.Domain.Interfaces.IServices;
using Polls.Domain.ViewModel.Session;
using System.Threading.Tasks;

namespace Polls.Web.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionServices _sessionServices;
        private readonly IToastNotification _toastNotification;

        public SessionController(ISessionServices sessionServices, IToastNotification toastNotification)
        {
            _sessionServices = sessionServices;
            _toastNotification = toastNotification;
        }

        // GET: SessionController
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> Index(int courseId)
        
        {
            ViewBag.CourseId = courseId;

            if (courseId == 0)
                return View(await _sessionServices.GeAllSessionsAsync());

            return View(await _sessionServices.GeAllSessionsInCourseAsync(courseId));
        }

        // GET: SessionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> Create(CreateSessionViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage("model state is not valid");
                return RedirectToAction(nameof(Index), new { courseId = viewModel.CourseId });
            }

            await _sessionServices.CreateSessionFromViewModelAsync(viewModel);

            return RedirectToAction(nameof(Index), new { courseId = viewModel.Viewbag });
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> Edit(EditSessionViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage("model state is not valid");
                return RedirectToAction(nameof(Index), new { courseId = viewModel.Viewbag });
            }

            await _sessionServices.UpDateSessionFromViewModelAsync(viewModel);

            return RedirectToAction(nameof(Index), new { courseId = viewModel.Viewbag });
        }
    }
}