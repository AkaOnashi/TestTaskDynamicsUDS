using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    public class WorkDayCalculator : IWorkDayCalculator
    {
        public DateTime Calculate(DateTime startDate, int dayCount, WeekEnd[] weekEnds)
        {
            var weekEndDates = new List<DateTime>();
            var requiredDate = startDate;

            if(weekEnds != null)
            {
                foreach (var weekEnd in weekEnds)
                {
                    for (var date = weekEnd.StartDate; date <= weekEnd.EndDate; date = date.AddDays(1))
                    {
                        weekEndDates.Add(date);
                    }
                }
            }

            for(int workDaysCount = 0; workDaysCount < dayCount - 1; requiredDate = requiredDate.AddDays(1))
            {
                if (!weekEndDates.Contains(requiredDate))
                {
                    workDaysCount++;
                }
            }

            return requiredDate;
        }
    }
}
