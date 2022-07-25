using Polls.Domain.ViewModel.Course;
using Polls.Domain.ViewModel.Instructor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.ViewModel.Home
{
    public class DashboardHomeViewModel
    {
        public int InstructorsCount { get; set; }
        public int CoursesCount { get; set; }
        public int StudentsPollsCount { get; set; }
        public int SessionsCount { get; set; }
        public List<CourseViewModel> Courses { get; set; }
        public List<InstructorIndexViewModel> Instructors { get; set; }
    }
}
