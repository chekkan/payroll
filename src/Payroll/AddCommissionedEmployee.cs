namespace Payroll
{
    public class AddCommissionedEmployee : AddEmployeeTransaction
    {
        private readonly double salary;
        private readonly double rate;

        public AddCommissionedEmployee(int empid,
                                       string name,
                                       string address,
                                       double salary,
                                       double rate) : base(empid, name, address)
        {
            this.salary = salary;
            this.rate = rate;
        }

        protected override PaymentClassification MakeClassification()
        {
            return new CommissionedClassification(salary, rate);
        }

        protected override PaymentSchedule MakeSchedule()
        {
            return new BiweeklySchedule();
        }
    }
}