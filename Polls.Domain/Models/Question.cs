using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Text { get; set; }

        // Foreign Key
        public int CourseId { get; set; }

        // Navigation Properties

        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; }
    }
}
