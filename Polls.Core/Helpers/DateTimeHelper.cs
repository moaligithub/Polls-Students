using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.Core.Helpers
{
    public class DateTimeHelper
    {
        public string GetDateFromDateTimeToString(DateTime dateTime)
            => dateTime.Day + " - " + dateTime.Month + " - " + dateTime.Year;
    }
}
