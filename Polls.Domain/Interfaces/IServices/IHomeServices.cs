using Polls.Domain.ViewModel.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.Interfaces.IServices
{
    public interface IHomeServices
    {
        /// <summary>
        ///     Get data for home index page > get courses count, get sessions count, get instructors count,
        ///     get polls count, get courses, get instructors.
        /// </summary>
        /// <param name="InstructorsCount">Expresses Instructors Count</param>
        /// <param name="CoursesCount">Expresses Courses Count</param>
        /// <returns>one of Home IndexViewModel.</returns>
        Task<HomeIndexViewModel> GetHomeIndexAsync(int InstructorsCount, int CoursesCount);

        /// <summary>
        ///     Get data for home dashboard page. 
        /// </summary>
        /// <returns>one of DashboardHomeViewModel.</returns>
        Task<DashboardHomeViewModel> GetDashboardHomeAsync();
    }
}
