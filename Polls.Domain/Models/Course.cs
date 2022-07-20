using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        [MinLength(50)]
        public string Description { get; set; }

        public int Views { get; set; }

        // Navigation Properties

        public ICollection<Department> Departments { get; set; }
        public ICollection<Session> Sessions { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<Poll> Polls { get; set; }
    }
}