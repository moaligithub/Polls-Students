using Polls.Domain.ViewModel.Course;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.ViewModel.Session
{
    public class CreateSessionViewModel : SessionViewModel
    {
        [Required]
        public int CourseId { get; set; }

        [Required]
        public int InstructorId { get; set; }

        public int Viewbag { get; set; }
    }
}
