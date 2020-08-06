using System;
using System.Collections.Generic;

namespace Payroll
{
    public class Paycheck
    {
        private readonly IDictionary<string, string> fields;

        public Paycheck(DateTime startDate, DateTime payDate)
        {
            fields = new Dictionary<string, string>();
            PayPeriodStartDate = startDate;
            PayPeriodEndDate = payDate;
        }

        public void SetField(string key, string value)
        {
            fields[key] = value;
        }

        public DateTime PayPeriodStartDate { get; }
        public DateTime PayPeriodEndDate { get; }
        public double GrossPay { get; set; }
        public double Deductions { get; set; }
        public double NetPay { get; set; }

        public string GetField(string field) => fields[field];
    }
}
