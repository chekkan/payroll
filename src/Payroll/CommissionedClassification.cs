using System;

namespace Payroll
{
    public class CommissionedClassification : PaymentClassification
    {
        private SalesReceipt salesReceipt;

        public CommissionedClassification(double salary, double commissionRate)
        {
            Salary = salary;
            CommissionRate = commissionRate;
        }

        public double Salary { get; private set; }
        public double CommissionRate { get; private set; }

        public SalesReceipt GetSalesReceipt(DateTime date)
        {
            return salesReceipt;
        }

        public double CalculatePay(Paycheck paycheck)
        {
            return Salary + (salesReceipt.Amount * CommissionRate);
        }

        public void AddSalesReceipt(SalesReceipt salesReceipt)
        {
            this.salesReceipt = salesReceipt;
        }
    }
}