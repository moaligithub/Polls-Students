using Microsoft.AspNetCore.Http;
using Polls.Domain.ViewModel.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.ViewModel.Instructor
{
    public class EditInstructorViewModel : InstructorViewModel
    {
        public int Id { get; set; }
        public EditContactViewModel Contact { get; set; }
        public IFormFile File { get; set; }
    }
}
