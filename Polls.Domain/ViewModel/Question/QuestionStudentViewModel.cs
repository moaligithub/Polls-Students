using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.ViewModel.Question
{
    public class QuestionStudentViewModel
    {
        [Required]
        [Display(Name = "Question")]
        public string Text { get; set; }

        [Display(Name = "Rating")]
        public string Rating { get; set; }
    }
}
