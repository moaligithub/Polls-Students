using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Domain.ViewModel.Question
{
    public class QuestionStatsViewModel
    {
        public int badPercentage { get; set; }
        public int goodPercentage { get; set; }
        public int veryGoodPercentage { get; set; }
    }
}
