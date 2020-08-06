using System;

namespace Payroll
{
    public class WeeklySchedule : PaymentSchedule
    {
        public bool IsPayDate(DateTime payDate)
        {
            return payDate.DayOfWeek == DayOfWeek.Friday;
        }

        public DateTime GetPeriodStartDate(DateTime payDate) =>
            payDate.AddDays(-6);
    }
}
