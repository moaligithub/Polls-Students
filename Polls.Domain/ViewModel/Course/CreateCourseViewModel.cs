using Microsoft.AspNetCore.Http;
using Polls.Domain.ViewModel.Department;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.ViewModel.Course
{
    public class CreateCourseViewModel : CourseViewModel
    {
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Departments")]
        public List<DepartmentCheckBoxViewModel> Departments { get; set; }
    }
}
