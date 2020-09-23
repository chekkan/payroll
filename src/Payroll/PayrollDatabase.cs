using System.Collections;
using System.Collections.Generic;

namespace Payroll
{
    public static class PayrollDatabase
    {
        private static readonly Hashtable Employees = new Hashtable();
        private static readonly Hashtable UnionMembers = new Hashtable();

        public static void AddEmployee(int id, Employee employee)
        {
            Employees[id] = employee;
        }

        internal static void DeleteEmployee(int id)
        {
            Employees.Remove(id);
        }

        public static void DeleteAll()
        {
            Employees.Clear();
            UnionMembers.Clear();
        }

        public static Employee GetEmployee(int id)
        {
            return Employees[id] as Employee;
        }

        public static IEnumerable<int> GetAllEmployeeIds()
        {
            ICollection ids = Employees.Keys;
            int[] result = new int[ids.Count];
            ids.CopyTo(result, 0);
            return result;
        }

        public static Employee GetUnionMember(int memberId)
        {
            return UnionMembers[memberId] as Employee;
        }

        public static void AddUnionMember(int id, Employee employee)
        {
            UnionMembers[id] = employee;
        }

        public static void RemoveUnionMember(int id)
        {
            UnionMembers.Remove(id);
        }
    }
}