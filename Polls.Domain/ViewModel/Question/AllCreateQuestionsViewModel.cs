using Polls.Domain.ViewModel.Course;
using Polls.Domain.ViewModel.Department;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.ViewModel.Question
{
    public class AllCreateQuestionsViewModel
    {
        public List<string> Questions { get; set; }
        public List<CourseViewModel> Courses { get; set; }
    }
}
