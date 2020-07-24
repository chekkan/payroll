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

            var sc = Assert.IsType<SalariedClassification>(e.Classification);
            Assert.Equal(1000.00, sc.Salary);

            Assert.IsType<MonthlySchedule>(e.Schedule);

            Assert.IsType<HoldMethod>(e.Method);
        }

        [Fact]
        public void AddHourlyEmployee()
        {
            int empId = 2;
            var t = new AddHourlyEmployee(empId, "Rob", "Work", 10.00);
            t.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.Equal("Rob", e.Name);

            var hc = Assert.IsType<HourlyClassification>(e.Classification);
            Assert.Equal(10.00, hc.HourlyRate);

            Assert.IsType<WeeklySchedule>(e.Schedule);

            Assert.IsType<HoldMethod>(e.Method);
        }

        [Fact]
        public void AddCommissionedEmployee()
        {
            int empId = 3;
            var t = new AddCommissionedEmployee(empId,
                                                "John",
                                                "Remote",
                                                900.00,
                                                10.00);
            t.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.Equal("John", e.Name);

            var cc = Assert.IsType<CommissionedClassification>(e.Classification);
            Assert.Equal(900.00, cc.Salary);
            Assert.Equal(10.00, cc.CommissionRate);

            Assert.IsType<BiweeklySchedule>(e.Schedule);

            Assert.IsType<HoldMethod>(e.Method);
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