namespace Payroll
{
    public class Bank
    {
    }

    public class Account
    {
    }

    public class DirectMethod : PaymentMethod
    {
        public DirectMethod(Bank bank, Account account)
        {

        }

        public void Pay(Paycheck paycheck)
        {
            throw new System.NotImplementedException();
        }
    }
}