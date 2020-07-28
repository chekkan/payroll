namespace Payroll
{
    public class ChangeDirectTransaction : ChangeMethodTransaction
    {
        private readonly Bank bank;
        private readonly Account account;

        public ChangeDirectTransaction(int id, Bank bank, Account account) : base(id)
        {
            this.bank = bank;
            this.account = account;
        }

        protected override PaymentMethod Method =>
            new DirectMethod(bank, account);
    }
}
