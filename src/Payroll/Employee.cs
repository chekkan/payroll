namespace Payroll
{
    public class Employee
    {
        public Employee(int empid, string name, string address)
        {
            Name = name;
        }

        public string Name { get; set; }
        public PaymentClassification Classification { get; set; }
        public PaymentSchedule Schedule { get; set; }
        public PaymentMethod Method { get; set; }
        public Affiliation Affiliation { get; set; }
    }
}