using System;
using System.Collections;

namespace Payroll
{
    public class PayrollDatabase
    {
        private static Hashtable employees = new Hashtable();
        private static Hashtable unionMembers = new Hashtable();

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

        internal static Employee GetUnionMember(int memberId)
        {
            return unionMembers[memberId] as Employee;
        }
        public static void AddUnionMember(int id, Employee employee)
        {
            unionMembers[id] = employee;
        }
    }
}