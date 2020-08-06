using System;

namespace Payroll
{
    public abstract class PaymentClassification
    {
        public abstract double CalculatePay(Paycheck paycheck);

        public bool IsInPayPeriod(DateTime theDate, Paycheck paycheck)
        {
            DateTime payPeriodEndDate = paycheck.PayPeriodEndDate;
            DateTime payPeriodStartDate = paycheck.PayPeriodStartDate;
            return (theDate >= payPeriodStartDate)
                && (theDate <= payPeriodEndDate);
        }
    }
}
