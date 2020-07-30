using System;

namespace Payroll
{
    public class SalesReceiptTransaction : Transaction
    {
        private readonly int empId;
        private readonly DateTime date;
        private readonly double amount;

        public SalesReceiptTransaction(int empId, DateTime date, double amount)
        {
            this.empId = empId;
            this.date = date;
            this.amount = amount;
        }

        public void Execute()
        {
            Employee e = PayrollDatabase.GetEmployee(empId);

            if (e != null)
            {
                var cc = e.Classification as CommissionedClassification;

                if (cc != null)
                    cc.AddSalesReceipt(new SalesReceipt(date, amount));
                else
                    throw new InvalidOperationException(
                           "Tried to add sales receipt to non-commissioned employee");
            }
        }
    }
}