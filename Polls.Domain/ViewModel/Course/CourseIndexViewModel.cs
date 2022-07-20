using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.ViewModel.Course
{
    public class CourseIndexViewModel : CourseViewModel
    {
        public int SessionsCount { get; set; }
        public string Date { get; set; }
        public int Views { get; set; }
        public int PollsCount { get; set; }
    }
}
