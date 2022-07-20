
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.ViewModel.Question
{
    public class EditQuestionViewModel : QuestionViewModel
    {
        public int CourseId { get; set; }
        public bool IsSelected { get; set; }
    }
}
