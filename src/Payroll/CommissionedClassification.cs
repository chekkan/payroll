using System;
using System.Collections.Generic;
using System.Linq;

namespace Payroll
{
    public class CommissionedClassification : PaymentClassification
    {
        private IDictionary<DateTime, SalesReceipt> salesReceipts;

        public CommissionedClassification(double salary, double commissionRate)
        {
            Salary = salary;
            CommissionRate = commissionRate;
            salesReceipts = new Dictionary<DateTime, SalesReceipt>();
        }

        public double Salary { get; private set; }
        public double CommissionRate { get; private set; }

        public SalesReceipt GetSalesReceipt(DateTime date) => salesReceipts[date];

        public override double CalculatePay(Paycheck paycheck) =>
            salesReceipts.Values
            .Where(r => IsInPayPeriod(r.Date, paycheck))
            .Sum(r => r.Amount)
            * CommissionRate
            + Salary;

        public void AddSalesReceipt(SalesReceipt salesReceipt)
        {
            this.salesReceipts.Add(salesReceipt.Date, salesReceipt);
        }
    }
}
