namespace Payroll
{
    public class CommissionedClassification : PaymentClassification
    {
        public CommissionedClassification(double salary, double commissionRate)
        {
            Salary = salary;
            CommissionRate = commissionRate;
        }

        public double Salary { get; private set; }
        public double CommissionRate { get; private set; }

        public double CalculatePay(Paycheck paycheck)
        {
            throw new System.NotImplementedException();
        }
    }
}