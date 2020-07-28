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
            var t = new AddCommissionedEmployee(empId, "John", "Remote", 900.00, 10.00);
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

        [Fact]
        public void TimeCardTransaction()
        {
            int empId = 5;

            var t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            t.Execute();

            var tct = new TimeCardTransaction(new DateTime(2005, 7, 31), 8.0, empId);
            tct.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.NotNull(e);

            var hc = Assert.IsType<HourlyClassification>(e.Classification);
            TimeCard tc = hc.GetTimeCard(new DateTime(2005, 7, 31));
            Assert.NotNull(tc);
            Assert.Equal(8.0, tc.Hours);
        }

        [Fact]
        public void AddServiceCharge()
        {
            int empId = 2;
            var t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            t.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.NotNull(e);

            int memberId = 86; // Maxwell Smart
            var af = new UnionAffiliation(memberId, 12.95);
            e.Affiliation = af;
            PayrollDatabase.AddUnionMember(memberId, e);

            var sct = new ServiceChargeTransaction(memberId,
                                                   new DateTime(2005, 8, 8),
                                                   12.95);
            sct.Execute();

            ServiceCharge sc = af.GetServiceCharge(new DateTime(2005, 8, 8));
            Assert.NotNull(sc);
            Assert.Equal(12.95, sc.Amount);
        }

        [Fact]
        public void ChangeNameTransaction()
        {
            int empId = 2;
            var t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            t.Execute();

            var cnt = new ChangeNameTransaction(empId, "Bob");
            cnt.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.NotNull(e);
            Assert.Equal("Bob", e.Name);
        }

        [Fact]
        public void ChangeAddressTransaction()
        {
            int empId = 3;
            var t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            t.Execute();

            var cat = new ChangeAddressTransaction(empId, "Work");
            cat.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.NotNull(e);
            Assert.Equal("Work", e.Address);
        }

        [Fact]
        public void ChangeHourlyTransaction()
        {
            int empId = 3;
            var t = new AddCommissionedEmployee(empId, "Lance", "Home", 2500, 3.2);
            t.Execute();

            var cht = new ChangeHourlyTransaction(empId, 27.52);
            cht.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.NotNull(e);

            PaymentClassification pc = e.Classification;
            Assert.NotNull(pc);
            var hc = Assert.IsType<HourlyClassification>(pc);
            Assert.Equal(27.52, hc.HourlyRate);

            PaymentSchedule ps = e.Schedule;
            Assert.IsType<WeeklySchedule>(ps);
        }

        [Fact]
        public void ChangeSalariedTransaction()
        {
            int empId = 4;
            var t = new AddCommissionedEmployee(empId, "Rebecca", "Home", 2600.0, 3.2);
            t.Execute();

            var cst = new ChangeSalariedTransaction(empId, 2500.0);
            cst.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.NotNull(e);

            PaymentClassification pc = e.Classification;
            Assert.NotNull(e);
            var sc = Assert.IsType<SalariedClassification>(pc);
            Assert.Equal(2500.0, sc.Salary);

            PaymentSchedule ps = e.Schedule;
            Assert.IsType<MonthlySchedule>(ps);
        }

        [Fact]
        public void ChangeCommissionedTransaction()
        {
            int empId = 5;
            var t = new AddSalariedEmployee(empId, "John", "Lost", 2300.0);
            t.Execute();

            var cct = new ChangeCommissionedTransaction(empId, 2200.0, 3.2);
            cct.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.NotNull(e);

            PaymentClassification pc = e.Classification;
            Assert.NotNull(pc);
            var cc = Assert.IsType<CommissionedClassification>(pc);
            Assert.Equal(2200.0, cc.Salary);
            Assert.Equal(3.2, cc.CommissionRate);

            PaymentSchedule ps = e.Schedule;
            Assert.IsType<BiweeklySchedule>(ps);
        }

        [Fact]
        public void ChangeDirectTransaction()
        {
            int empId = 6;
            var t = new AddSalariedEmployee(empId, "John", "Home", 2300.0);
            t.Execute();

            var bank = new Bank();
            var account = new Account();
            var cdt = new ChangeDirectTransaction(empId, bank, account);
            cdt.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.NotNull(e);

            PaymentMethod pm = e.Method;
            Assert.IsType<DirectMethod>(pm);
        }

        [Fact]
        public void ChangeMailTransaction()
        {
            int empId = 7;
            var t = new AddCommissionedEmployee(empId, "Rich", "Home", 2100.0, 10.0);
            t.Execute();

            var cmt = new ChangeMailTransaction(empId, "Somewhere");
            cmt.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.NotNull(e);

            PaymentMethod pm = e.Method;
            var mm = Assert.IsType<MailMethod>(pm);
            Assert.Equal("Somewhere", mm.Address);
        }

        [Fact]
        public void ChangeHoldTransaction()
        {
            int empId = 8;
            var t = new AddSalariedEmployee(empId, "John", "Home", 2200.0);
            t.Execute();

            var cht = new ChangeHoldTransaction(empId);
            cht.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.NotNull(e);

            PaymentMethod pm = e.Method;
            Assert.IsType<HoldMethod>(pm);
        }

        [Fact]
        public void ChangeUnionMember()
        {
            int empId = 8;
            var t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            t.Execute();
            int memberId = 7743;
            var cmt = new ChangeMemberTransaction(empId, memberId, 99.42);
            cmt.Execute();
            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.NotNull(e);

            Affiliation affiliation = e.Affiliation;
            Assert.NotNull(affiliation);
            var uf = Assert.IsType<UnionAffiliation>(affiliation);
            Assert.Equal(99.42, uf.Dues);
            Employee member = PayrollDatabase.GetUnionMember(memberId);
            Assert.NotNull(member);
            Assert.Same(e, member);

        }
    }
}