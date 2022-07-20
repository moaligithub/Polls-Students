using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polls.Domain.Interfaces.IServices;
using Polls.Domain.ViewModel.Instructor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polls.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorServices _instructorServices;

        public InstructorController(IInstructorServices instructorServices)
        {
            _instructorServices = instructorServices;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int Result = await _instructorServices.DeleteAsync(id);

            if (Result == 404)
                return NotFound("This instructor is not found");

            if (Result == 400)
                return BadRequest();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<InstructorIndexViewModel> instructors = await _instructorServices.GetAllAsync();

            if (instructors == null)
                return NotFound();
            
            return Ok(instructors);
        }
    }
}
