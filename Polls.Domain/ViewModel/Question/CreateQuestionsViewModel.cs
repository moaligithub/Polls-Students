using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.ViewModel.Question
{
    public class CreateQuestionsViewModel
    {
        public List<string> Questions { get; set; }
        public int CourseId { get; set; }
    }
}
