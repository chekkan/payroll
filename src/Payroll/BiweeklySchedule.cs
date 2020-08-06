using System;
using System.Collections.Generic;
using System.Linq;

namespace Payroll
{
    public class BiweeklySchedule : PaymentSchedule
    {
        public bool IsPayDate(DateTime payDate) =>
            IsSecondFriday(payDate) || IsFourthFriday(payDate);

        public DateTime GetPeriodStartDate(DateTime payDate) =>
            IsSecondFriday(payDate)
                ? FourthFridayOfMonth(payDate.AddMonths(-1)).AddDays(1)
                : SecondFridayOfMonth(payDate).AddDays(1);

        private bool IsSecondFriday(DateTime date) =>
            IsFriday(date) && SecondFridayOfMonth(date) == date.Date;

        private DateTime SecondFridayOfMonth(DateTime aDate) =>
            FridaysOfMonth(aDate).Skip(1).First();

        private bool IsFourthFriday(DateTime date) =>
            IsFriday(date) && FourthFridayOfMonth(date) == date.Date;

        private DateTime FourthFridayOfMonth(DateTime aDate) =>
            FridaysOfMonth(aDate).Skip(3).First();

        private IEnumerable<DateTime> FridaysOfMonth(DateTime date)
        {
            var firstDay = new DateTime(date.Year, date.Month, 1);
            var nextMonth = date.AddMonths(1);
            var nextMonthFirstDay = new DateTime(nextMonth.Year, nextMonth.Month, 1);
            var lastDay = nextMonthFirstDay.AddDays(-1);
            return GetFridays(firstDay, lastDay);
        }

        private IEnumerable<DateTime> GetFridays(DateTime startdate, DateTime enddate)
        {
            // step forward to the first friday
            while (!IsFriday(startdate))
                startdate = startdate.AddDays(1);

            while (startdate <= enddate)
            {
                yield return startdate;
                startdate = startdate.AddDays(7);
            }
        }

        private bool IsFriday(DateTime date) => date.DayOfWeek == DayOfWeek.Friday;
    }
}
