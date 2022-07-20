using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polls.Domain.Const;
using Polls.Domain.Interfaces.IServices;
using Polls.Domain.ViewModel.Department;
using System.Threading.Tasks;

namespace Polls.Web.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentServices _departmentServices;

        public DepartmentController(IDepartmentServices departmentServices)
        {
            _departmentServices = departmentServices;
        }

        // GET: DepartmentController
        public async Task<ActionResult> Index()
        {
            return View(await _departmentServices.GetAllDepartmentAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            await _departmentServices.CreateDepartmentFromViewModelAsync(viewModel);

            return RedirectToAction(nameof(Index));
        }
    }
}
