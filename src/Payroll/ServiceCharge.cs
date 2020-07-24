using System;

namespace Payroll
{
    public class ServiceCharge
    {
        private readonly DateTime time;

        public ServiceCharge(DateTime time, double amount)
        {
            this.time = time;
            Amount = amount;
        }

        public double Amount { get; }
    }
}