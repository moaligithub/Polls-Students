using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.ViewModel.Question
{
    public class QuestionViewModel
    {
        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [MaxLength(200)]
        public string Text { get; set; }
    }
}
