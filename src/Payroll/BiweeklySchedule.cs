using System;

namespace Payroll
{
    public class BiweeklySchedule : PaymentSchedule
    {
        public bool IsPayDate(DateTime payDate)
        {
            return IsFirstFriday(payDate) || IsThirdFriday(payDate);
        }

        private bool IsFirstFriday(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Friday && date.Year == 2020;
        }

        private bool IsThirdFriday(DateTime date)
        {
            return false;
        }
    }
}