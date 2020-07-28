namespace Payroll
{
    public class ChangeCommissionedTransaction : ChangeClassificationTransaction
    {
        private readonly double salary;
        private readonly double hourlyRate;

        public ChangeCommissionedTransaction(int id, double salary, double hourlyRate)
            : base(id)
        {
            this.salary = salary;
            this.hourlyRate = hourlyRate;
        }

        protected override PaymentClassification Classification =>
            new CommissionedClassification(salary, hourlyRate);

        protected override PaymentSchedule Schedule =>
            new BiweeklySchedule();
    }
}