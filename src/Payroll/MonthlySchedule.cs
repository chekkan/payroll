using System;

namespace Payroll
{
    public class MonthlySchedule : PaymentSchedule
    {
        private bool IsLastDayOfMonth(DateTime date)
        {
            int m1 = date.Month;
            int m2 = date.AddDays(1).Month;
            return (m1 != m2);
        }

        public bool IsPayDate(DateTime payDate) => IsLastDayOfMonth(payDate);

        public DateTime GetPeriodStartDate(DateTime payDate) =>
            new DateTime(payDate.Year, payDate.Month, 1);
    }
}
