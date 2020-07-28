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
    }
}