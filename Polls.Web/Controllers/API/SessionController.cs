using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polls.Domain.Const;
using Polls.Domain.Interfaces.IServices;
using Polls.Domain.ViewModel.Session;
using System.Threading.Tasks;

namespace Polls.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    public class SessionController : ControllerBase
    {
        private readonly ISessionServices _sessionServices;

        public SessionController(ISessionServices sessionServices)
        {
            _sessionServices = sessionServices;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int Result = await _sessionServices.DeleteSessionAsync(id);

            if (Result == 404)
                return NotFound("Session is not found");

            if (Result == 400)
                return BadRequest();

            return Ok();
        }
    }
}