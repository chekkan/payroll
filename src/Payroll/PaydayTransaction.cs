using System;
using System.Collections;

namespace Payroll
{
    public class PaydayTransaction : Transaction
    {
        private readonly DateTime payDate;
        private static Hashtable paychecks = new Hashtable();

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
                if (employee.IsPayDate(payDate))
                {
                    Paycheck pc = new Paycheck(payDate);
                    paychecks[empId] = pc;
                    employee.Payday(pc);
                }
            }
        }

        public Paycheck GetPaycheck(int empId)
        {
            return paychecks[empId] as Paycheck;
        }
    }
}