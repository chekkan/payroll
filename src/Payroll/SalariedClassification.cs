namespace Payroll
{
    public class SalariedClassification : PaymentClassification
    {
        public SalariedClassification(double salary)
        {
            Salary = salary;
        }

        public double Salary { get; set; }
    }
}