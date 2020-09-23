using System;

namespace Payroll
{
    public class MonthlySchedule : PaymentSchedule
    {
        public bool IsPayDate(DateTime payDate) => IsLastDayOfMonth(payDate);

        public DateTime GetPeriodStartDate(DateTime payDate) =>
            new DateTime(payDate.Year, payDate.Month, 1);

        private static bool IsLastDayOfMonth(DateTime date)
        {
            int m1 = date.Month;
            int m2 = date.AddDays(1).Month;
            return (m1 != m2);
        }
    }
}
