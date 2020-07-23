namespace Payroll
{
    public class AddCommissionedEmployee : AddEmployeeTransaction
    {
        public AddCommissionedEmployee(int empid,
                                       string name,
                                       string address,
                                       double salary,
                                       double rate) : base(empid, name, address)
        {
        }

        protected override PaymentClassification MakeClassification()
        {
            return new CommisionedClassification();
        }

        protected override PaymentSchedule MakeSchedule()
        {
            return new BiweeklySchedule();
        }
    }
}