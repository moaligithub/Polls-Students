using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Polls.Domain.Const;
using Polls.Domain.Interfaces.IServices;
using Polls.Domain.ViewModel.Poll;
using Polls.Domain.ViewModel.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Polls.Web.Controllers
{
    public class PollController : Controller
    {
        private readonly IPollServices _pollServices;
        private readonly ICourseServices _courseServices;
        private readonly IToastNotification _toastNotification;

        public PollController(IPollServices pollServices, ICourseServices courseServices,
            IToastNotification toastNotification)
        {
            _pollServices = pollServices;
            _courseServices = courseServices;
            _toastNotification = toastNotification;
        }

        // GET: PollController
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> Index()
        {            
            return View(await _pollServices.GetAllBasePollAsync());
        }

        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> PollStudentsIndex(int courseId, string courseName)
        {
            List<PollStudentViewModel> polls = await _pollServices.GetStudentsPollsInCourseAsync(courseId);
            
            if (polls == null)
                return View(@"Views/Shared/error-404.cshtml");

            ViewBag.CourseName = courseName;    
            return View(polls);
        }

        // GET: PollController/Details/5
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> Details(int id)
        {
            PollStudentDetailsViewModel viewModel = await _pollServices.GetPollStudentDetailsAsync(id);

            if(viewModel == null)
                return View(@"Views/Shared/error-404.cshtml");

            return View(viewModel);
        }

        // POST: PollController/Create
        [HttpPost]
        [Authorize(Roles = Roles.Student)]
        public async Task<ActionResult> Create(CreatePollStudentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage("model state is not valid");
                return RedirectToAction("GetPollQuestions", "Poll", new { courseId = viewModel.CourseId });
            }

            string StudentName = User.FindFirst("FullName").Value;
            string StudentId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string StudentDepartment = User.FindFirst("Department").Value;

            bool Result = await _pollServices.CreatePollStudentAsync(viewModel, StudentName, StudentId, StudentDepartment);
            
            if (!Result)
                return RedirectToAction("GetPollQuestions", "Question", new { courseId = viewModel.CourseId });

            return RedirectToAction(nameof(SuccessSubmit));
        }

        [Authorize(Roles = Roles.Student)]
        public IActionResult SuccessSubmit()
        {
            return View();
        }
    }
}
