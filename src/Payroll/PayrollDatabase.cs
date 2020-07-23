using System;
using System.Collections;

namespace Payroll
{
    public class PayrollDatabase
    {
        private static Hashtable employees = new Hashtable();

        public static void AddEmployee(int id, Employee employee)
        {
            employees[id] = employee;
        }

        internal static void DeleteEmployee(int id)
        {
            employees.Remove(id);
        }

        public static Employee GetEmployee(int id)
        {
            return employees[id] as Employee;
        }
    }
}
