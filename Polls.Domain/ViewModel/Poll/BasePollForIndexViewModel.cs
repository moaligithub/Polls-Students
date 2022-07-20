using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.ViewModel.Poll
{
    public class BasePollForIndexViewModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int PollsStudentCount { get; set; }
        public int QuestionsCount { get; set; }
    }
}
