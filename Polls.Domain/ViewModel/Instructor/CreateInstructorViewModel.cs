using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.ViewModel.Instructor
{
    public class CreateInstructorViewModel : InstructorViewModel    
    {
        public ContactViewModel Contact { get; set; }
        public IFormFile File { get; set; }
    }
}
