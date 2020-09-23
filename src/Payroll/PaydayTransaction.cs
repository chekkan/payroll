using System;
using System.Collections;

namespace Payroll
{
    public class PaydayTransaction : Transaction
    {
        private readonly DateTime payDate;
        private static readonly Hashtable Paychecks = new Hashtable();

        public PaydayTransaction(DateTime payDate)
        {
            this.payDate = payDate;
        }

        public void Execute()
        {
            var empIds = PayrollDatabase.GetAllEmployeeIds();

            foreach (int empId in empIds)
            {
                Employee employee = PayrollDatabase.GetEmployee(empId);
                if (!employee.IsPayDate(payDate))
                    continue;

                DateTime startDate = employee.GetPayPeriodStartDate(payDate);
                Paycheck pc = new Paycheck(startDate, payDate);
                Paychecks[empId] = pc;
                employee.Payday(pc);
            }
        }

        public static Paycheck GetPaycheck(int empId)
        {
            return Paychecks[empId] as Paycheck;
        }
    }
}
