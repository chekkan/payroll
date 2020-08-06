using System;

namespace Payroll
{
    public class ServiceCharge
    {
        public ServiceCharge(DateTime time, double amount)
        {
            Time = time;
            Amount = amount;
        }

        public DateTime Time { get; }
        public double Amount { get; }
    }
}
