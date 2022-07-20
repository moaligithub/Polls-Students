using Polls.Domain.ViewModel.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.Interfaces.IServices
{
    public interface ICourseServices
    {
        /// <summary>
        ///     Get all courses in department by department id.
        /// </summary>
        /// <param name="departmentId">Expresses department id.</param>
        /// <returns>List of courses type of CourseIndexViewModel</returns>
        Task<List<CourseIndexViewModel>> GetCoursesInDepartmentAsync(int departmentId);

        /// <summary>
        ///     Get all courses 
        /// </summary>
        /// <returns>List of courses type of CourseIndexViewModel</returns>
        Task<List<CourseIndexViewModel>> GetAllCoursesAsync();

        /// <summary>
        ///     Create a new course from CreateCourseViewModel.
        /// </summary>
        /// <param name="courseViewModel">Expresses CreateCourseViewModel.</param>
        /// <returns>True in case create successfully, or False in case create failed.</returns>
        Task<bool> CreateCourseFromViewModel(CreateCourseViewModel courseViewModel);

        /// <summary>
        ///     Get course for update this course. 
        /// </summary>
        /// <param name="id">Expresses course id.</param>
        /// <returns>one of course type of CreateCourseViewModel.</returns>
        Task<CreateCourseViewModel> GetCourseForEditAsync(int id);

        /// <summary>
        ///     Update course from CreateCourseViewModel.
        /// </summary>
        /// <param name="editCourseView">Expresses CreateCourseViewModel.</param>
        /// <returns>True in case update successfully, or False in case update failed.</returns>
        Task<bool> UpDateCourseFromViewModelAsync(CreateCourseViewModel editCourseView);

        /// <summary>
        ///     Delete course by course id.
        /// </summary>
        /// <param name="id">Expresses course id.</param>
        /// <returns>200 in delete successfully, or 404 in course not found, or 400 in bad request.</returns>
        Task<int> DeleteCourseAsync(int id);

        /// <summary>
        ///     Get all courses for drop list async.
        /// </summary>
        /// <returns>list of courses type of CourseViewModel.</returns>
        Task<List<CourseViewModel>> GetAllCourseForDropListAsync();

        /// <summary>
        ///     Get course details by course id.
        /// </summary>
        /// <param name="id">Expresses course id.</param>
        /// <returns>One of course type of CourseDetailsViewModel.</returns>
        Task<CourseDetailsViewModel> GetCourseDetailsAsync(int id);

        /// <summary>
        ///     Get all courses for home page.
        /// </summary>
        /// <returns>List of courses type of CourseHomeViewModel</returns>
        Task<List<CourseHomeViewModel>> GetAllCoursesForHomeAsync();
    }
}
