using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.ViewModel.Instructor
{
    public class InstructorReviewViewModel
    {
        [MaxLength(300)]
        [MinLength(10)]
        public string Description { get; set; }

        [MaxLength(5)]
        [Required]
        public int Review { get; set; }

        [Required]
        public string StudentId { get; set; }
    }
}
