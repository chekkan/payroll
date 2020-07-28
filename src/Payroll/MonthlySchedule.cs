using System;

namespace Payroll
{
    public class MonthlySchedule : PaymentSchedule
    {
        public bool IsPayDate(DateTime payDate)
        {
            return true;
        }
    }
}