namespace Payroll
{
    public class ChangeAddressTransaction : ChangeEmployeeTransaction
    {
        private readonly string newAddress;

        public ChangeAddressTransaction(int empId, string newAddress)
            : base(empId)
        {
            this.newAddress = newAddress;
        }

        protected override void Change(Employee e)
        {
            e.Address = newAddress;
        }
    }
}