using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.ViewModel.Instructor
{
    public class InstructorViewModel
    {
        [Required]
        [MaxLength(20)]
        [Display(Name = "FIRST NAME *")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "LAST NAME *")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "JOB TITLE *")]
        [DataType(DataType.Text)]
        public string JobTitle { get; set; }
    }
}
