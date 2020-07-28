using System;

namespace Payroll
{
    public class BiweeklySchedule : PaymentSchedule
    {
        public bool IsPayDate(DateTime payDate) => false;
    }
}