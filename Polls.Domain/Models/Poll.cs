using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.Models
{
    public class Poll
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string StudentId { get; set; }

        [Required]
        public string StudentName { get; set; }
        
        [Required]
        public string StudentDepartment { get; set; }

        // Foreign Key
     
        [Required]
        public int CourseId { get; set; }

        // Navigation Properties
       
        public ICollection<QuestionPoll> Questions { get; set; }

        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; }
    }
}
