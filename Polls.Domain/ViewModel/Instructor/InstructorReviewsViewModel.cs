using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.ViewModel.Instructor
{
    public class InstructorReviewsViewModel
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

        public ContactViewModel Contact { get; set; }
        public InstructorReviewViewModel StudentReview { get; set; }
        public List<InstructorReviewViewModel> InstructorReviews { get; set; }
    }
}
