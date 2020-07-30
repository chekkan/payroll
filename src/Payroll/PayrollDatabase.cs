using System;
using System.Collections;
using System.Linq;

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

        public static void DeleteAll()
        {
            employees.Clear();
            unionMembers.Clear();
        }

        public static Employee GetEmployee(int id)
        {
            return employees[id] as Employee;
        }

        public static int[] GetAllEmployeeIds()
        {
            ICollection ids = employees.Keys;
            int[] result = new int[ids.Count];
            ids.CopyTo(result, 0);
            return result;
        }

        public static Employee GetUnionMember(int memberId)
        {
            return unionMembers[memberId] as Employee;
        }

        public static void AddUnionMember(int id, Employee employee)
        {
            unionMembers[id] = employee;
        }

        public static void RemoveUnionMember(int id)
        {
            unionMembers.Remove(id);
        }
    }
}