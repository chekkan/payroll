using System;

namespace Payroll
{
    public class SalariedClassification : PaymentClassification
    {
        public SalariedClassification(double salary)
        {
            Salary = salary;
        }

        public double Salary { get; }

        public override double CalculatePay(Paycheck paycheck) => Salary;
    }
}
