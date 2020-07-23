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
            AddSalariedEmployee t = new AddSalariedEmployee(empId,
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
    }
}
