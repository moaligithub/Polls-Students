using Polls.Domain.ViewModel.Poll;
using Polls.Domain.ViewModel.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.Interfaces.IServices
{
    public interface IPollServices
    {
        /// <summary>
        ///     Get all base poll.
        /// </summary>
        /// <returns>List of Poll type of BasePollForIndexViewModel</returns>
        Task<List<BasePollForIndexViewModel>> GetAllBasePollAsync();

        /// <summary>
        ///     Get all students polls in course by course id. 
        /// </summary>
        /// <param name="courseId">Expresses course id.</param>
        /// <returns>List of polls type of PollStudentViewModel</returns>
        Task<List<PollStudentViewModel>> GetStudentsPollsInCourseAsync(int courseId);

        /// <summary>
        ///  Create a new poll student from CreatePollStudentViewModel.
        /// </summary>
        /// <param name="createPollStudent">Expresses CreatePollStudentViewModel.</param>
        /// <param name="StudentName">Expresses StudentName.</param>
        /// <param name="StudentId">Expresses StudentId.</param>
        /// <param name="StudentDepartment">Expresses StudentDepartment.</param>
        /// <returns>True in case create successfully, or False in case create failed.</returns>
        Task<bool> CreatePollStudentAsync(CreatePollStudentViewModel createPollStudent, string StudentName, string StudentId, string StudentDepartment);

        /// <summary>
        ///     Get poll student details by poll id.
        /// </summary>
        /// <param name="pollId">Expresses poll id.</param>
        /// <returns>one of poll type of PollStudentDetailsViewModel.</returns>
        Task<PollStudentDetailsViewModel> GetPollStudentDetailsAsync(int pollId);
    }
}
