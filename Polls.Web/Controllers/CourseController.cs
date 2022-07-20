using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Polls.Domain.Const;
using Polls.Domain.Interfaces.IServices;
using Polls.Domain.ViewModel.Course;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Polls.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseServices _courseServices;
        private readonly IDepartmentServices _departmentServices;
        private readonly IToastNotification _toastNotification;

        public CourseController(ICourseServices courseServices, IDepartmentServices departmentServices, 
            IToastNotification toastNotification)
        {
            _courseServices = courseServices;
            _departmentServices = departmentServices;
            _toastNotification = toastNotification;
        }

        // GET: CourseController
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> Index(int departmentId)
        {
            if (departmentId > 0)
                return View(await _courseServices.GetCoursesInDepartmentAsync(departmentId));
            
            return View(await _courseServices.GetAllCoursesAsync());
        }

        // GET: CourseController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (User.Identity.IsAuthenticated && User.FindFirst(ClaimTypes.Role).Value == Roles.Admin)
                return RedirectToAction("Index", "Home");

            CourseDetailsViewModel viewModel = await _courseServices.GetCourseDetailsAsync(id);

            if (viewModel == null)
                return View(@"Views/Shared/error-404.cshtml");

            return View(viewModel);
        }

        // GET: CourseController/Create
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> Create()
        {
            CreateCourseViewModel createCourseViewModel = new CreateCourseViewModel
            {
                Departments = await _departmentServices.GetAllDepartmentCheckBoxAsync()
            };

            return View(createCourseViewModel);
        }

        // POST: CourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> Create(CreateCourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage("model state is not valid!");
                return RedirectToAction(nameof(Create));
            }

            bool Result = await _courseServices.CreateCourseFromViewModel(viewModel);

            if (!Result)
                return RedirectToAction(nameof(Create));

            return RedirectToAction(nameof(Index));
        }

        // GET: CourseController/Edit/5
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> Edit(int id)
        {
            CreateCourseViewModel viewModel = await _courseServices.GetCourseForEditAsync(id);

            if (viewModel == null)
                return View(@"Views/Shared/error-404.cshtml");

            return View(viewModel);
        }

        // POST: CourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> Edit(CreateCourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage("model state is not valid");
                return RedirectToAction(nameof(Edit), new { id = viewModel.Id });
            }

            bool Result = await _courseServices.UpDateCourseFromViewModelAsync(viewModel);

            if (!Result)
                return RedirectToAction(nameof(Edit), new { id = viewModel.Id });

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetAllCourses()
        {
            List<CourseHomeViewModel> courses = await _courseServices.GetAllCoursesForHomeAsync();

            if (courses.Count == 0)
                return View(@"Views/Shared/error-404.cshtml");

            return View(courses);
        }
    }
}