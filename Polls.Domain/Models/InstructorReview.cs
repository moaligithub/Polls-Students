using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.Models
{
    public class InstructorReview
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(300)]
        [MinLength(10)]
        public string Description { get; set; }

        [MaxLength(5)]
        [Required]
        public int Review { get; set; }

        [Required]
        public string StudentId { get; set; }

        // Foreign Key
        public int InstructorId { get; set; }

        // Navigation Properties

        [ForeignKey(nameof(InstructorId))]
        public Instructor Instructor { get; set; }
    }
}
