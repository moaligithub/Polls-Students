using Polls.Domain.ViewModel.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.Interfaces.IServices
{
    public interface ISessionServices
    {
        /// <summary>
        ///     Get all sessions for index action.
        /// </summary>
        /// <returns>sessions type of AllSessionsIndexViewModel</returns>
        Task<AllSessionsIndexViewModel> GeAllSessionsAsync();

        /// <summary>
        ///     Get all sessions in course by course id for index action.
        /// </summary>
        /// <param name="courseId">Expresses Course Id.</param>
        /// <returns>sessions type of AllSessionsIndexViewModel</returns>
        Task<AllSessionsIndexViewModel> GeAllSessionsInCourseAsync(int courseId);

        /// <summary>
        ///     Craete a new session from CreateSessionViewModel.
        /// </summary>
        /// <param name="sessionViewModel">Expresses CreateSessionViewModel.</param>
        /// <returns>True in case create successfully, or False in case create failed.</returns>
        Task<bool> CreateSessionFromViewModelAsync(CreateSessionViewModel sessionViewModel);

        /// <summary>
        ///     Update session from EditSessionViewModel.
        /// </summary>
        /// <param name="sessionViewModel">Expresses EditSessionViewModel.</param>
        /// <returns>True in case update successfully, or False in case update failed.</returns>
        Task<bool> UpDateSessionFromViewModelAsync(EditSessionViewModel sessionViewModel);

        /// <summary>
        ///     Delete session from db by session id.
        /// </summary>
        /// <param name="id">Expresses Session Id.</param>
        /// <returns>200 in case delete successfully, or 404 in case session is not found, or 400 in bad request.</returns>
        Task<int> DeleteSessionAsync(int id);
    }
}