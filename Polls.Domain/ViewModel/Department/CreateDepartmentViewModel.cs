using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.ViewModel.Department
{
    public class CreateDepartmentViewModel
    {
        [MaxLength(50)]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
