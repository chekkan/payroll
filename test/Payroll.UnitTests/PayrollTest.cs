using System;
using Xunit;

namespace Payroll.UnitTests
{
    public class PayrollTest
    {
        [Fact]
        public void AddSalariedEmployee()
        {
            int empId = 1;
            var t = new AddSalariedEmployee(empId, "Bob", "Home", 1000.00);
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
        public void AddHourlyEmployee()
        {
            int empId = 2;
            var t = new AddHourlyEmployee(empId, "Rob", "Work", 10.00);
            t.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.Equal("Rob", e.Name);

            PaymentClassification pc = e.Classification;
            Assert.True(pc is HourlyClassification);

            HourlyClassification hc = pc as HourlyClassification;

            Assert.Equal(10.00, hc.HourlyRate);
            PaymentSchedule ps = e.Schedule;
            Assert.True(ps is WeeklySchedule);

            PaymentMethod pm = e.Method;
            Assert.True(pm is HoldMethod);
        }

        [Fact]
        public void DeleteEmployee()
        {
            int empId = 4;
            var t = new AddCommissionedEmployee(empId, "Bill", "Home", 2500, 3.2);
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