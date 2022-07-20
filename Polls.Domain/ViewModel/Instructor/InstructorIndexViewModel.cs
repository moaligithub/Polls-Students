using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.ViewModel.Instructor
{
    public class InstructorIndexViewModel : InstructorViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Total Review")]
        public float TotalReview { get; set; }
    }
}
