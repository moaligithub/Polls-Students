using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Polls.Domain.Const;
using Polls.Domain.Interfaces.IServices;
using Polls.Domain.Models;
using Polls.Domain.ViewModel.Instructor;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Polls.Web.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IInstructorServices _instructorServices;
        private readonly IToastNotification _toastNotification;
        public InstructorController(IInstructorServices instructorServices, IToastNotification toastNotification)
        {
            _instructorServices = instructorServices;
            _toastNotification = toastNotification;
        }

        // GET: InstructorController
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> Index()
        {
            return View(await _instructorServices.GetAllAsync());
        }

        // GET: InstructorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: InstructorController/Create
        [Authorize(Roles = Roles.Admin)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: InstructorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> Create(CreateInstructorViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage("model state is not valid");
                return View();
            }

            bool Result = await _instructorServices.CreateInstructorFromViewModelAsync(viewModel);

            if (!Result)
                return View();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Review(int instructorId)
        {
            if (User.Identity.IsAuthenticated && User.FindFirst(ClaimTypes.Role).Value == Roles.Admin)
                return RedirectToAction("Index", "Home");
            
            InstructorReviewsViewModel viewModel;

            if (User.Identity.IsAuthenticated)
                viewModel = await _instructorServices.GetInstructorReviewsAsync(instructorId, User.FindFirst(ClaimTypes.NameIdentifier).Value);
            
            else
                viewModel = await _instructorServices.GetInstructorReviewsAsync(instructorId);
            
            if (viewModel == null)
                return View(@"Views/Shared/error-404.cshtml");

            return View(viewModel);
        }
        
        [HttpPost]
        [Authorize(Roles = Roles.Student)]
        public async Task<ActionResult> CreateReview(CreateInstructorReview viewModel)
        {
            if (!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage("model state is not valid");
                return RedirectToAction(nameof(Review), new { instructorId = viewModel.InstructorId });
            }

            await _instructorServices.CreateInstructorReviewAsync(viewModel, User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return RedirectToAction(nameof(Review), new { instructorId = viewModel.InstructorId });
        }

        [HttpPost]
        [Authorize(Roles = Roles.Student)]
        public async Task<ActionResult> EditReview(CreateInstructorReview viewModel)
        {
            if (!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage("model state is not valid");
                return RedirectToAction(nameof(Review), new { instructorId = viewModel.InstructorId });
            }

            await _instructorServices.UpDateInstructorReviewAsync(viewModel, User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return RedirectToAction(nameof(Review), new { instructorId = viewModel.InstructorId });
        }

        // GET: InstructorController/Edit/5
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> Edit(int id)
        {
            EditInstructorViewModel instructor = await _instructorServices.GetInstructorForEditAsync(id);
            
            if (instructor is null)
                return View(@"Views/Shared/error-404.cshtml");
            
            return View(instructor);
        }

        // POST: InstructorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        public ActionResult Edit(EditInstructorViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage("model state is not valid");
                return RedirectToAction(nameof(Edit), new { id = viewModel.Id });
            }    

            bool Result = _instructorServices.UpDateInstructorFromViewModel(viewModel); 

            if(!Result)
                return RedirectToAction(nameof(Edit), new { id = viewModel.Id });

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetAllInstructors()
        {
            if (User.Identity.IsAuthenticated && User.FindFirst(ClaimTypes.Role).Value == Roles.Admin)
                return RedirectToAction("Index", "Home");

            List<InstructorHomeViewModel> instructors = await _instructorServices.GetAllInstructorsForHomeAsync();

            if (instructors.Count == 0)
                return View(@"Views/Shared/error-404.cshtml");

            return View(instructors);
        }
    }
}
