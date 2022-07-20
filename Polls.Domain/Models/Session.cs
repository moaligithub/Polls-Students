using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.Models
{
    public class Session
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MinLength(20)]
        public string Description { get; set; }

        // Foreign Key

        [Required]
        public int CourseId { get; set; }

        [Required]
        public int InstructorId { get; set; }

        // Navigation Properties

        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; }

        [ForeignKey(nameof(InstructorId))]
        public Instructor Instructor { get; set; }
    }
}   