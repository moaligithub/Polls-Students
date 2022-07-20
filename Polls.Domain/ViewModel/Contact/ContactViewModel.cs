using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.ViewModel.Instructor
{
    public class ContactViewModel
    {
        [Display(Name = "FACEBOOK")]
        [DataType(DataType.Text)]
        public string Facebook { get; set; }

        [Display(Name = "TWITTER")]
        [DataType(DataType.Text)]
        public string Twitter { get; set; }

        [Display(Name = "LINKED IN")]
        [DataType(DataType.Text)]
        public string LinkedIn { get; set; }

        [Display(Name = "EMAIL *")]
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}
