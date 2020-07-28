namespace Payroll
{
    public class HoldMethod : PaymentMethod
    {
        public void Pay(Paycheck paycheck)
        {
            paycheck.SetField("Disposition", "Hold");
        }
    }
}