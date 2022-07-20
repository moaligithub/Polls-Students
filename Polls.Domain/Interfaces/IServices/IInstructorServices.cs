using Polls.Domain.ViewModel.Instructor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.Interfaces.IServices
{
    public interface IInstructorServices
    {
        /// <summary>
        ///     Get All Instructors for index action.
        /// </summary>
        /// <returns>List of instructors type of InstructorIndexViewModel.</returns>
        Task<List<InstructorIndexViewModel>> GetAllAsync();

        /// <summary>
        ///     Create a new instructor from CreateInstructorViewModel.
        /// </summary>
        /// <param name="instructorViewModel">Expresses CreateInstructorViewModel.</param>
        /// <returns>True in case create successfully, or False in case create failed.</returns>
        Task<bool> CreateInstructorFromViewModelAsync(CreateInstructorViewModel instructorViewModel);

        /// <summary>
        ///     Get instructor for update by instructor id.
        /// </summary>
        /// <param name="id">Expresses instructor id.</param>
        /// <returns>one instructor type of EditInstructorViewModel.</returns>
        Task<EditInstructorViewModel> GetInstructorForEditAsync(int id);

        /// <summary>
        ///     Update instructor from EditInstructorViewModel.
        /// </summary>
        /// <param name="editInstructor">Expresses EditInstructorViewModel.</param>
        /// <returns>True in case update successfully, or False in case Update failed.</returns>
        bool UpDateInstructorFromViewModel(EditInstructorViewModel editInstructor);

        /// <summary>
        ///     Delete Instructor By Instructor Id.
        /// </summary>
        /// <param name="id">Expresses Instructor Id.</param>
        /// <returns>200 in case delete successfully, or 404 in case not found, 400 in case bad request.</returns>
        Task<int> DeleteAsync(int id);

        /// <summary>
        ///     Get instructor reviews by instructor id.
        /// </summary>
        /// <param name="instructorId">Expresses instructor id.</param>
        /// <param name="studentId">Expresses student id.</param>
        /// <returns>InstructorReviewsViewModel.</returns>
        Task<InstructorReviewsViewModel> GetInstructorReviewsAsync(int instructorId, string studentId = null);

        /// <summary>
        ///     Create instructor review from CreateInstructorReviewAsync.
        /// </summary>
        /// <param name="review">Expresses CreateInstructorReviewAsync.</param>
        /// <param name="studentId">Expresses studentId.</param>
        /// <returns>True in case create successfully, or False in case create failed.</returns>
        Task<bool> CreateInstructorReviewAsync(CreateInstructorReview review, string studentId);

        /// <summary>
        ///     UpDate instructor review from CreateInstructorReviewAsync.
        /// </summary>
        /// <param name="review">Expresses CreateInstructorReviewAsync.</param>
        /// <param name="studentId">Expresses studentId.</param>
        /// <returns>True in case update successfully, or False in case update failed.</returns>
        Task<bool> UpDateInstructorReviewAsync(CreateInstructorReview review, string studentId);

        /// <summary>
        ///     Get all instructors for home page.
        /// </summary>
        /// <returns>list of instructors type of InstructorHomeViewModel.</returns>
        Task<List<InstructorHomeViewModel>> GetAllInstructorsForHomeAsync();
    }
}