using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.ViewModel.Department
{
    public class DepartmentIndexViewModel : DepartmentViewModel
    {
        [Display(Name = "Courses")]
        public int CoursesCount { get; set; }
    }
}
