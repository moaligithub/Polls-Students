using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string LinkedIn { get; set; }
        public string Email { get; set; }

        // Foreign Key

        [Required]
        public int InstructorId { get; set; }

        // Navigation Properties

        [ForeignKey(nameof(InstructorId))]
        public Instructor Instructor { get; set; }
    }
}
