using System;

namespace Payroll
{
    public interface PaymentSchedule
    {
        bool IsPayDate(DateTime payDate);

        DateTime GetPeriodStartDate(DateTime payDate);
    }
}
