namespace Payroll
{
    public abstract class AddEmployeeTransaction : Transaction
    {
        private readonly int empId;
        private readonly string name;
        private readonly string address;

        protected AddEmployeeTransaction(int empId, string name, string address)
        {
            this.empId = empId;
            this.name = name;
            this.address = address;
        }

        protected abstract PaymentClassification MakeClassification();
        protected abstract PaymentSchedule MakeSchedule();

        public void Execute()
        {
            PaymentClassification pc = MakeClassification();
            PaymentSchedule ps = MakeSchedule();
            PaymentMethod pm = new HoldMethod();

            Employee e = new Employee(empId, name, address)
            {
                Classification = pc, 
                Schedule = ps,
                Method = pm, 
                Affiliation = new NoAffiliation()
            };
            PayrollDatabase.AddEmployee(empId, e);
        }
    }
}