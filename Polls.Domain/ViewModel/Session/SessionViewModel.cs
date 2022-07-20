using Polls.Domain.ViewModel.Course;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.ViewModel.Session
{
    public class SessionViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        [MinLength(20)]
        public string Description { get; set; }
    }
}
