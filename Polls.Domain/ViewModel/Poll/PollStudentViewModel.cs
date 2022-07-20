using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.ViewModel.Poll
{
    public class PollStudentViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Student Id")]
        public string StudentId { get; set; }

        [Display(Name = "Student Name")]
        public string StudentName { get; set; }
        
        [Display(Name = "Student Department")]
        public string StudentDepartment { get; set; }
    }
}
