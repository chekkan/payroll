using System;
using Xunit;

namespace Payroll.UnitTests
{
    public class PayrollTest
    {
        [Fact]
        public void TestAddSalariedEmployee()
        {
            int empId = 1;
            var t = new AddSalariedEmployee(empId,
                                            "Bob",
                                            "Home",
                                            1000.00);
            t.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.Equal("Bob", e.Name);
            PaymentClassification pc = e.Classification;

            Assert.True(pc is SalariedClassification);
            SalariedClassification sc = pc as SalariedClassification;

            Assert.Equal(1000.00, sc.Salary);
            PaymentSchedule ps = e.Schedule;
            Assert.True(ps is MonthlySchedule);

            PaymentMethod pm = e.Method;
            Assert.True(pm is HoldMethod);
        }

        [Fact]
        public void DeleteEmployee()
        {
            int empId = 4;
            var t = new AddCommissionedEmployee(empId,
                                                "Bill",
                                                "Home",
                                                2500,
                                                3.2);
            t.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.NotNull(e);

            var dt = new DeleteEmployeeTransaction(empId);
            dt.Execute();

            e = PayrollDatabase.GetEmployee(empId);
            Assert.Null(e);
        }
    }
}
