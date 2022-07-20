using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace Polls.Domain.Interfaces.IServices
{
    public interface IUserServices
    {
        /// <summary>
        ///     Check of user login by user id and user password.
        /// </summary>
        /// <param name="UserId">Expresses user id.</param>
        /// <param name="Password">Expresses user password.</param>
        /// <returns>ClaimsPrincipal in case login successfully, or null in case login failed</returns>
        ClaimsPrincipal Login(string UserId, string Password);
    }
}