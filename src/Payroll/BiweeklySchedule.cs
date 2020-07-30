using System;
using System.Collections.Generic;
using System.Linq;

namespace Payroll
{
    public class BiweeklySchedule : PaymentSchedule
    {
        public bool IsPayDate(DateTime payDate) =>
            IsSecondFriday(payDate) || IsFourthFriday(payDate);

        private bool IsSecondFriday(DateTime date) =>
            IsFriday(date) && FridaysOfMonth(date).Skip(1).First() == date.Date;

        private bool IsFourthFriday(DateTime date) =>
            IsFriday(date) && FridaysOfMonth(date).Skip(3).First() == date.Date;

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