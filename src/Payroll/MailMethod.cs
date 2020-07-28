namespace Payroll
{
    public class MailMethod : PaymentMethod
    {
        public MailMethod(string address)
        {
            Address = address;
        }

        public string Address { get; }

        public void Pay(Paycheck paycheck)
        {
            throw new System.NotImplementedException();
        }
    }
}