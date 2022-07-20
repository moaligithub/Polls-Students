using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.Models
{
    public class Instructor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string JobTitle { get; set; }
        public byte[] ProfilePicture { get; set; }

        public float TotalReview { get; set; }

        // Navigation Properties

        public Contact Contact  { get; set; }
        public ICollection<Session> Sessions { get; set; }
        public ICollection<InstructorReview> Reviews { get; set; }

    }
}
