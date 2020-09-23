namespace Payroll
{
    public abstract class ChangeMethodTransaction : ChangeEmployeeTransaction
    {
        protected ChangeMethodTransaction(int id) : base(id)
        {
        }

        protected override void Change(Employee e)
        {
            e.Method = Method;
        }

        protected abstract PaymentMethod Method { get; }
    }
}