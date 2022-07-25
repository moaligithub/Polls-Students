using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polls.Domain.Const;
using Polls.Domain.Interfaces.IServices;
using System.Threading.Tasks;

namespace Polls.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentServices _departmentServices;

        public DepartmentController(IDepartmentServices departmentServices)
        {
            _departmentServices = departmentServices;
        }

        [HttpPut("{id}/{newName}")]
        public async Task<IActionResult> Edit(int id, string newName)
        {
            int Result = await _departmentServices.EditDepartmentNameAsync(id, newName);

            if (Result == 400)
                return BadRequest();

            if (Result == 404)
                return NotFound("Department is not found!");
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int Result = await _departmentServices.DeleteAsync(id);

            if (Result == 404)
                return NotFound("Department is not found!");

            if (Result == 400)
                return BadRequest();

            return Ok();
        }
    }
}
