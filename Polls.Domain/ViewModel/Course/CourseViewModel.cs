using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.ViewModel.Course
{
    public class CourseViewModel
    {
        [Required]
        [MaxLength(100)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Id")]
        public int Id { get; set; }

        public bool IsSelected { get; set; }
    }
}
