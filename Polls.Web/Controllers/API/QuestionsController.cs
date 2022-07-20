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
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionServices _questionServices;

        public QuestionsController(IQuestionServices questionServices)
        {
            _questionServices = questionServices;
        }

        [HttpDelete("{courseId}")]
        public async Task<ActionResult> DeleteByCourseId(int courseId)
        {
            int Result = await _questionServices.DeleteQuestionInCourseAsync(courseId);

            if (Result == 404)
                return NotFound("questions is not found.");

            if (Result == 400)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("One/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            int Result = await _questionServices.DeleteOneQuestionAsync(id);

            if (Result == 404)
                return NotFound("question is not found.");

            if (Result == 400)
                return BadRequest();

            return Ok();
        }
    }
}
