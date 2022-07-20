using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.ViewModel.Question
{
    public class QuestionCountViewModel
    {
        [Required]
        [Display(Name = "Write Questions Count")]
        public int Count { get; set; }
        public int CourseId { get; set; }
    }
}
