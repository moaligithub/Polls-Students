using Polls.Domain.ViewModel.Course;
using Polls.Domain.ViewModel.Instructor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.ViewModel.Session
{
    public class SessionIndexViewModel : CreateSessionViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Course")]
        public string CourseName { get; set; }

        [Display(Name = "Instructor")]
        public string InstructorName { get; set; }
    }
}
