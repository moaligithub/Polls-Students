using Polls.Domain.ViewModel.Question;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.ViewModel.Poll
{
    public class CreatePollStudentViewModel
    {
        [Required]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public List<QuestionStudentViewModel> Questions { get; set; }
    }
}
