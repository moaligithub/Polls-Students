using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.ViewModel.Department
{
    public class DepartmentViewModel : CreateDepartmentViewModel
    {
        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; }
    }
}
