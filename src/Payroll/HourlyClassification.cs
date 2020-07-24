namespace Payroll
{
    public class HourlyClassification : PaymentClassification
    {
        public HourlyClassification(double hourlyRate)
        {
            HourlyRate = hourlyRate;
        }

        public double HourlyRate { get; private set; }
    }
}