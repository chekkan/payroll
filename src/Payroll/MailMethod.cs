namespace Payroll
{
    public class MailMethod : PaymentMethod
    {
        public MailMethod(string address)
        {
            Address = address;
        }

        public string Address { get; }
    }
}