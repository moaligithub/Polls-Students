using Polls.Domain.ViewModel.Course;
using Polls.Domain.ViewModel.Instructor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.ViewModel.Session
{
    public class EditSessionViewModel : CreateSessionViewModel
    {
        public int Id { get; set; }
        public List<CourseViewModel> Courses { get; set; }
        public List<InstructorIndexViewModel> Instructors { get; set; }
    }
}
