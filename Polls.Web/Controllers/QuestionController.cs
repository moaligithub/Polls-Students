using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Polls.Domain.Const;
using Polls.Domain.Interfaces.IServices;
using Polls.Domain.ViewModel.Poll;
using Polls.Domain.ViewModel.Question;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polls.Web.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionServices _questionServices;
        private readonly ICourseServices _courseServices;
        private readonly IToastNotification _toastNotification;

        public QuestionController(IQuestionServices questionServices, ICourseServices courseServices,
            IToastNotification toastNotification)
        {
            _questionServices = questionServices;
            _courseServices = courseServices;
            _toastNotification = toastNotification;
        }

        [Authorize(Roles = Roles.Admin)]
        public ActionResult Create(QuestionCountViewModel viewModel)
        {
            ViewBag.MaxQuesions = viewModel.Count;

            CreateQuestionsViewModel questionsViewModel = new CreateQuestionsViewModel()
            {
                CourseId = viewModel.CourseId,
            };

            return View(questionsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> Create(CreateQuestionsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage("model state is not valid");

                ViewBag.MaxQuesions = viewModel.Questions.Count;
                return View(viewModel);
            }

            bool  Result = await _questionServices.CreateQuestionsForCourseAsync(viewModel);

            if (!Result)
            {
                ViewBag.MaxQuesions = viewModel.Questions.Count;
                return View(viewModel);
            }

            return RedirectToAction("Index", "Poll");
        }

        // GET: QuestionController/Create
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> CreateAll(QuestionCountViewModel countViewModel)
        {
            AllCreateQuestionsViewModel viewModel = new AllCreateQuestionsViewModel
            {
                Courses = await _courseServices.GetAllCourseForDropListAsync()
            };

            ViewBag.MaxQuesions = countViewModel.Count;
            return View(viewModel);
        }

        // POST: QuestionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> CreateAll(AllCreateQuestionsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage("model state is not valid");

                ViewBag.MaxQuesions = viewModel.Questions.Count;
                return View(viewModel);
            }

            bool Result = await _questionServices.CreateQuestionsForCoursesAsync(viewModel);

            if (!Result)
            {
                ViewBag.MaxQuesions = viewModel.Questions.Count;
                return View(viewModel);
            }

            return RedirectToAction("Index", "Poll");
        }

        // GET: QuestionController/Edit/5
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> Edit(int courseId, string courseName, int pollStudents)
        {
            ViewBag.courseName = courseName;
            ViewBag.pollStudentsCount = pollStudents;
            return View(await _questionServices.GetQuestionsForEditAsync(courseId));
        }

        // POST: QuestionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        public ActionResult Edit(List<EditQuestionViewModel> viewModels, string courseName, int pollStudents)
        { 
            if (!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage("model state is not valid");
                return RedirectToAction(nameof(Edit), new
                {
                    courseId = viewModels[0].CourseId,
                    courseName = courseName,
                    pollStudents = pollStudents
                });
            }

            bool Result = _questionServices.UpDateQuestionsFromViewModel(viewModels);
            
            if (!Result)
                return RedirectToAction(nameof(Edit), new
                {
                    courseId = viewModels[0].CourseId,
                    courseName = courseName,
                    pollStudents = pollStudents
                });

            return RedirectToAction("Index", "Poll");
        }

        [HttpGet]
        [Authorize(Roles = Roles.Student)]
        public async Task<IActionResult> GetPollQuestions(int courseId)
        {
            CreatePollStudentViewModel viewModel = await _questionServices.GetQuestionForCreatePollAsync(courseId);

            if (viewModel == null)
                return View(@"Views/Shared/error-404.cshtml");

            return View(viewModel);
        }
    }
}