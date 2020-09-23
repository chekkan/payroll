using System;

namespace Payroll
{
    public abstract class PaymentClassification
    {
        public abstract double CalculatePay(Paycheck paycheck);

        protected static bool IsInPayPeriod(DateTime theDate, Paycheck paycheck)
        {
            DateTime payPeriodEndDate = paycheck.PayPeriodEndDate;
            DateTime payPeriodStartDate = paycheck.PayPeriodStartDate;
            return DateUtil.IsInPayPeriod(theDate, payPeriodStartDate, payPeriodEndDate);
        }
    }
}
