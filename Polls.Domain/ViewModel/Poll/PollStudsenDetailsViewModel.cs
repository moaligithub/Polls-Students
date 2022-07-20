using Polls.Domain.ViewModel.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.ViewModel.Poll
{
    public class PollStudentDetailsViewModel : PollStudentViewModel
    {
        public string CourseName { get; set; }
        public int CourseId { get; set; }
        public List<QuestionStudentViewModel> Questions { get; set; }
    }
}
