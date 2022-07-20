using Polls.Domain.ViewModel.Course;
using Polls.Domain.ViewModel.Instructor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.ViewModel.Home
{
    public class HomeIndexViewModel
    {
        public int CoursesCount { get; set; }
        public int SessionsCount { get; set; }
        public int InstructorsCount { get; set; }
        public int BasePollsCount { get; set; }
        public List<InstructorHomeViewModel> Instructors { get; set; }
        public List<CourseHomeViewModel> Courses { get; set; }
    }
}
