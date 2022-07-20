using Polls.Domain.ViewModel.Poll;
using Polls.Domain.ViewModel.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.Interfaces.IServices
{
    public interface IQuestionServices
    {
        /// <summary>
        ///     Create a new questions for Collection of course from AllCreateQuestionsViewModel.
        /// </summary>
        /// <param name="viewModel">Expresses AllCreateQuestionsViewModel.</param>
        /// <returns>True in case create successfully, or False in case create failed.</returns>
        Task<bool> CreateQuestionsForCoursesAsync(AllCreateQuestionsViewModel viewModel);

        /// <summary>
        ///     Create a new questions for one course from CreateQuestionsViewModel.
        /// </summary>
        /// <param name="viewModels">Expresses CreateQuestionsViewModel.</param>
        /// <returns>True in case create successfully, or False in case create failed.</returns>
        Task<bool> CreateQuestionsForCourseAsync(CreateQuestionsViewModel viewModel);

        /// <summary>
        ///     Get questions in course for edit by course id.
        /// </summary>
        /// <param name="courseId">Expresses course id.</param>
        /// <returns>List of questions type of EditQuestionViewModel.</returns>
        Task<List<EditQuestionViewModel>> GetQuestionsForEditAsync(int courseId);

        /// <summary>
        ///     Update Collection of questions from list of EditQuestionViewModel.
        /// </summary>
        /// <param name="editQuestions">Expresses List of EditQuestionViewModel.</param>
        /// <returns>True in case update successfully, or False in case update failed.</returns>
        bool UpDateQuestionsFromViewModel(List<EditQuestionViewModel> editQuestions);

        /// <summary>
        ///     Delete questions in course by course id.
        /// </summary>
        /// <param name="courseId">Expresses course id.</param>
        /// <returns>200 in case delete successfully, or 404 in case question not found, or 400 in case bad request.</returns>
        Task<int> DeleteQuestionInCourseAsync(int courseId);

        /// <summary>
        ///     Delete one question by question id.
        /// </summary>
        /// <param name="questionId">Expresses question id.</param>
        /// <returns>200 in case delete successfully, or 404 in case question not found, or 400 in case bad request.</returns>
        Task<int> DeleteOneQuestionAsync(int questionId);


        /// <summary>
        ///     Get Questions in course for poll student by course id.
        /// </summary>
        /// <param name="courseId">Expresses course id.</param>
        /// <returns>CreatePollStudentViewModel.</returns>
        Task<CreatePollStudentViewModel> GetQuestionForCreatePollAsync(int courseId);
    }
}