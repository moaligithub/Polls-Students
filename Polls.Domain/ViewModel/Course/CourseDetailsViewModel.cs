using Polls.Domain.ViewModel.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.ViewModel.Course
{
    public class CourseDetailsViewModel : CourseHomeViewModel
    {
        public List<string> Departments { get; set; }
        public bool Poll { get; set; }
        public int SessionsCount { get; set; }
        public List<SessionViewModel> Sessions { get; set; }
    }
}
