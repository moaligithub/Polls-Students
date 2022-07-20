using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.ViewModel.Instructor
{
    public class CreateInstructorReview
    {
        [MaxLength(300)]
        [MinLength(10)]
        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; }

        [Required]
        public int Review { get; set; }

        public int InstructorId { get; set; }
    }
}
