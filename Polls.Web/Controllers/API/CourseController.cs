using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polls.Domain.Const;
using Polls.Domain.Interfaces.IServices;
using Polls.Domain.ViewModel.Course;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polls.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    public class CourseController : ControllerBase
    {
        private readonly ICourseServices _courseServices;

        public CourseController(ICourseServices courseServices)
        {
            _courseServices = courseServices;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            int Result = await _courseServices.DeleteCourseAsync(id);

            if (Result == 404)
                return NotFound("This course is not found");

            if (Result == 400)
                return BadRequest();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<CourseViewModel> Courses = await _courseServices.GetAllCourseForDropListAsync();

            if (Courses == null)
                return NotFound();

            return Ok(Courses);
        }
    }
}
