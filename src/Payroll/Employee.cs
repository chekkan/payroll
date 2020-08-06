using System;

namespace Payroll
{
    public class Employee
    {
        public Employee(int empid, string name, string address)
        {
            Name = name;
            Address = address;
        }

        public string Name { get; set; }
        public string Address { get; set; }
        public PaymentClassification Classification { get; set; }
        public PaymentSchedule Schedule { get; set; }
        public PaymentMethod Method { get; set; }
        public Affiliation Affiliation { get; set; }

        public bool IsPayDate(DateTime payDate) => Schedule.IsPayDate(payDate);

        public void Payday(Paycheck paycheck)
        {
            double grossPay = Classification.CalculatePay(paycheck);
            double deductions = Affiliation.CalculateDeduction(paycheck);
            double netPay = grossPay - deductions;
            paycheck.GrossPay = grossPay;
            paycheck.Deductions = deductions;
            paycheck.NetPay = netPay;
            Method.Pay(paycheck);
        }

        public DateTime GetPayPeriodStartDate(DateTime payDate) =>
            Schedule.GetPeriodStartDate(payDate);
    }
}
