using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.ViewModel.Instructor
{
    public class InstructorHomeViewModel : InstructorIndexViewModel
    {
        [Display(Name = "Contact")]
        public ContactViewModel Contact { get; set; }
    }
}
