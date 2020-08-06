using System;
using Xunit;

namespace Payroll.UnitTests
{
    public class PayrollTest
    {
        const int SalariedEmployeeId = 1;
        const int CommissionedEmployeeId = 3;
        const int HourlyEmployeeId = 2;

        public PayrollTest()
        {
            PayrollDatabase.DeleteAll();
        }

        [Fact]
        public void AddSalariedEmployee()
        {
            int empId = SetupSalariedEmployee();
            Employee e = PayrollDatabase.GetEmployee(empId);

            Assert.Equal("John", e.Name);

            var sc = Assert.IsType<SalariedClassification>(e.Classification);
            Assert.Equal(2300.0, sc.Salary);

            Assert.IsType<MonthlySchedule>(e.Schedule);

            Assert.IsType<HoldMethod>(e.Method);
        }

        [Fact]
        public void AddHourlyEmployee()
        {
            int empId = SetupHourlyEmployee();
            Employee e = PayrollDatabase.GetEmployee(empId);

            Assert.Equal("Bill", e.Name);

            var hc = Assert.IsType<HourlyClassification>(e.Classification);
            Assert.Equal(15.25, hc.HourlyRate);

            Assert.IsType<WeeklySchedule>(e.Schedule);
            Assert.IsType<HoldMethod>(e.Method);
        }

        [Fact]
        public void AddCommissionedEmployee()
        {
            int empId = SetupCommissionedEmployee();
            Employee e = PayrollDatabase.GetEmployee(empId);

            Assert.Equal("Bob", e.Name);

            var cc = Assert.IsType<CommissionedClassification>(e.Classification);
            Assert.Equal(1500.0, cc.Salary);
            Assert.Equal(2.5, cc.CommissionRate);

            Assert.IsType<BiweeklySchedule>(e.Schedule);
            Assert.IsType<HoldMethod>(e.Method);
        }

        [Fact]
        public void DeleteEmployee()
        {
            int empId = SetupCommissionedEmployee();

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
            int empId = SetupHourlyEmployee();
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
        public void SalesReceiptTransaction()
        {
            int empId = SetupCommissionedEmployee();
            var date = new DateTime(2020, 7, 27);
            var amount = 490_000.0;

            var srt = new SalesReceiptTransaction(empId, date, amount);
            srt.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.NotNull(e);

            var pc = Assert.IsType<CommissionedClassification>(e.Classification);
            SalesReceipt sr = pc.GetSalesReceipt(date);
            Assert.NotNull(sr);
            Assert.Equal(date, sr.Date);
            Assert.Equal(amount, sr.Amount);
        }

        [Fact]
        public void AddServiceCharge()
        {
            int empId = SetupHourlyEmployee();

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
            int empId = SetupHourlyEmployee();

            var cnt = new ChangeNameTransaction(empId, "Bob");
            cnt.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.NotNull(e);
            Assert.Equal("Bob", e.Name);
        }

        [Fact]
        public void ChangeAddressTransaction()
        {
            int empId = SetupHourlyEmployee();

            var cat = new ChangeAddressTransaction(empId, "Work");
            cat.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.NotNull(e);
            Assert.Equal("Work", e.Address);
        }

        [Fact]
        public void ChangeHourlyTransaction()
        {
            int empId = SetupCommissionedEmployee();

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
            int empId = SetupCommissionedEmployee();

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
            int empId = SetupSalariedEmployee();

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
            int empId = SetupSalariedEmployee();
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
            int empId = SetupCommissionedEmployee();

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
            int empId = SetupSalariedEmployee();

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
            int empId = SetupHourlyEmployee();
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

        [Fact]
        public void ChangeUnaffiliatedTransaction()
        {
            var empId = SetupHourlyEmployee();
            // Make sure the employee is union affiliated
            int memberId = 7743;
            var cmt = new ChangeMemberTransaction(empId, memberId, 99.42);
            cmt.Execute();

            var cut = new ChangeUnaffiliatedTransaction(empId);
            cut.Execute();

            Employee e = PayrollDatabase.GetEmployee(empId);
            Assert.NotNull(e);

            Affiliation affiliation = e.Affiliation;
            Assert.NotNull(affiliation);

            var nf = Assert.IsType<NoAffiliation>(affiliation);
            Employee member = PayrollDatabase.GetUnionMember(memberId);
            Assert.Null(member);
        }

        [Fact]
        public void PaySingleSalariedEmployee()
        {
            int empId = SetupSalariedEmployee();
            var payDate = new DateTime(2001, 11, 30);

            var pt = new PaydayTransaction(payDate);
            pt.Execute();
            ValidateNoDeductionPaycheck(pt, empId, payDate, 2300.0);
        }

        [Fact]
        public void PaySingleSalariedEmployeeOnWrongDate()
        {
            int empId = SetupSalariedEmployee(SalariedEmployeeId + 10);
            var payDate = new DateTime(2001, 11, 29);

            var pt = new PaydayTransaction(payDate);
            pt.Execute();

            Paycheck pc = pt.GetPaycheck(empId);
            Assert.Null(pc);
        }

        [Fact]
        public void PayingSingleHourlyEmployeeNoTimeCards()
        {
            int empId = SetupHourlyEmployee();
            var payDate = new DateTime(2001, 11, 9); // Friday

            var pt = new PaydayTransaction(payDate);
            pt.Execute();

            ValidateNoDeductionPaycheck(pt, empId, payDate, 0.0);
        }

        [Fact]
        public void PaySingleHourlyEmployeeOneTimeCard()
        {
            int empId = SetupHourlyEmployee();
            var payDate = new DateTime(2001, 11, 9); // Friday

            var tc = new TimeCardTransaction(payDate, 2.0, empId);
            tc.Execute();

            var pt = new PaydayTransaction(payDate);
            pt.Execute();

            ValidateNoDeductionPaycheck(pt, empId, payDate, 30.5);
        }

        [Fact]
        public void PaySingleHourlyEmployeeOvertimeOneTimeCard()
        {
            int empId = SetupHourlyEmployee();
            var payDate = new DateTime(2001, 11, 9); // Friday

            var tc = new TimeCardTransaction(payDate, 9.0, empId);
            tc.Execute();

            var pt = new PaydayTransaction(payDate);
            pt.Execute();

            ValidateNoDeductionPaycheck(pt, empId, payDate, (8 + 1.5) * 15.25);
        }

        [Fact]
        public void PaySingleHourlyEmployeeOnWrongDate()
        {
            int empId = SetupHourlyEmployee();
            var payDate = new DateTime(2001, 11, 8); // Thursday

            var tc = new TimeCardTransaction(payDate, 9.0, empId);
            tc.Execute();

            var pt = new PaydayTransaction(payDate);
            pt.Execute();

            Paycheck pc = pt.GetPaycheck(empId);
            Assert.Null(pc);
        }

        [Fact]
        public void PaySingleHourlyEmployeeTwoTimeCards()
        {
            int empId = SetupHourlyEmployee();
            var payDate = new DateTime(2001, 11, 9); // Friday

            var tc = new TimeCardTransaction(payDate, 2.0, empId);
            tc.Execute();
            var tc2 = new TimeCardTransaction(payDate.AddDays(-1), 5.0, empId);
            tc2.Execute();

            var pt = new PaydayTransaction(payDate);
            pt.Execute();

            ValidateNoDeductionPaycheck(pt, empId, payDate, 7 * 15.25);
        }

        [Fact]
        public void PaySingleHourlyEmployeeWithTimeCardsSpanningTwoPayPeriods()
        {
            int empId = SetupHourlyEmployee();
            var payDate = new DateTime(2001, 11, 9); // Friday
            var dateInPreviousPayPeriod = new DateTime(2001, 11, 2);

            var tc = new TimeCardTransaction(payDate, 2.0, empId);
            tc.Execute();
            var tc2 = new TimeCardTransaction(dateInPreviousPayPeriod, 5.0, empId);
            tc2.Execute();

            var pt = new PaydayTransaction(payDate);
            pt.Execute();

            ValidateNoDeductionPaycheck(pt, empId, payDate, 2 * 15.25);
        }

        [Fact]
        public void PayCommissionedEmployeeWithOneSale()
        {
            int empId = SetupCommissionedEmployee();
            var payDate = new DateTime(2020, 7, 10); // Last Day

            var srt = new SalesReceiptTransaction(empId, payDate.AddDays(-1), 40_000);
            srt.Execute();

            var pt = new PaydayTransaction(payDate);
            pt.Execute();

            ValidateNoDeductionPaycheck(pt, empId, payDate, 40_000 * 2.5 + 1500);
        }

        [Fact]
        public void PayCommissionedEmployeeWithOneSaleWrongDate()
        {
            int empId = SetupCommissionedEmployee();
            var payDate = new DateTime(2020, 7, 3); // First Friday

            var srt = new SalesReceiptTransaction(empId, payDate.AddDays(-1), 40_000);
            srt.Execute();

            var pt = new PaydayTransaction(payDate);
            pt.Execute();

            Paycheck paycheck = pt.GetPaycheck(empId);
            Assert.Null(paycheck);
        }

        [Fact]
        public void PayCommissionedEmployeeWithOneSaleThirdFriday()
        {
            int empId = SetupCommissionedEmployee(CommissionedEmployeeId + 10);
            var payDate = new DateTime(2020, 7, 24); // Fourth Friday

            var srt = new SalesReceiptTransaction(empId, payDate.AddDays(-1), 50_000);
            srt.Execute();

            var pt = new PaydayTransaction(payDate);
            pt.Execute();

            ValidateNoDeductionPaycheck(pt, empId, payDate, 50_000 * 2.5 + 1500);
        }

        [Fact]
        public void PayCommissionedEmployeeWithTwoSales()
        {
            int empId = SetupCommissionedEmployee();
            var payDate = new DateTime(2020, 7, 24); // Fourth Friday

            var srt1 = new SalesReceiptTransaction(empId, payDate.AddDays(-1), 50_000);
            srt1.Execute();
            var srt2 = new SalesReceiptTransaction(empId, payDate.AddDays(-2), 10_000);
            srt2.Execute();

            var pay = (50_000 + 10_000) * 2.5 + 1500;

            var pt = new PaydayTransaction(payDate);
            pt.Execute();

            ValidateNoDeductionPaycheck(pt, empId, payDate, pay);
        }

        [Fact]
        public void PayCommissionedEmployeeWithSalesReceiptsSpanningTwoPayPeriods()
        {
            int empId = SetupCommissionedEmployee();
            var payDate = new DateTime(2020, 7, 24); // Fourth Friday
            var dateInPreviousPayPeriod = new DateTime(2020, 7, 9);

            var srt1 = new SalesReceiptTransaction(empId, payDate.AddDays(-1), 50_000);
            srt1.Execute();
            var srt2 = new SalesReceiptTransaction(empId,
                                                   dateInPreviousPayPeriod,
                                                   10_000);
            srt2.Execute();

            var pay = 50_000 * 2.5 + 1500;

            var pt = new PaydayTransaction(payDate);
            pt.Execute();

            ValidateNoDeductionPaycheck(pt, empId, payDate, pay);
        }

        [Fact]
        public void SalariedUnionMemberDues()
        {
            int empId = SetupSalariedEmployee();
            int memberId = 7734;
            var cmt = new ChangeMemberTransaction(empId, memberId, 9.42);
            cmt.Execute();
            var payDate = new DateTime(2001, 11, 30);
            var pt = new PaydayTransaction(payDate);
            pt.Execute();

            Paycheck pc = pt.GetPaycheck(empId);
            Assert.NotNull(pc);
            Assert.Equal(payDate, pc.PayPeriodEndDate);
            Assert.Equal(2300.0, pc.GrossPay);
            Assert.Equal("Hold", pc.GetField("Disposition"));
            Assert.Equal(5 * 9.42, pc.Deductions);
            Assert.Equal(2300.0 - 5 * 9.42, pc.NetPay);
        }

        [Fact]
        public void HourlyUnionMemberServiceCharge()
        {
            int empId = SetupHourlyEmployee();
            int memberId = 7734;
            var cmt = new ChangeMemberTransaction(empId, memberId, 9.42);
            cmt.Execute();

            var payDate = new DateTime(2001, 11, 9);
            var sct = new ServiceChargeTransaction(memberId, payDate, 19.42);
            sct.Execute();

            var tct = new TimeCardTransaction(payDate, 8.0, empId);
            tct.Execute();

            var pt = new PaydayTransaction(payDate);
            pt.Execute();

            Paycheck pc = pt.GetPaycheck(empId);
            Assert.NotNull(pc);
            Assert.Equal(payDate, pc.PayPeriodEndDate);
            Assert.Equal(8 * 15.25, pc.GrossPay);
            Assert.Equal("Hold", pc.GetField("Disposition"));
            Assert.Equal(9.42 + 19.42, pc.Deductions);
            Assert.Equal((8 * 15.25) - (9.42 + 19.42), pc.NetPay);
        }

        [Fact]
        public void ServiceChargesSpanningMultiplePayPeriods()
        {
            int empId = SetupHourlyEmployee();
            int memberId = 7734;
            var cmt = new ChangeMemberTransaction(empId, memberId, 9.42);
            cmt.Execute();

            var payDate = new DateTime(2001, 11, 9);
            var earlyDate = new DateTime(2001, 11, 2); // previous Friday
            var lateDate = new DateTime(2001, 11, 16); // next Friday

            var sct = new ServiceChargeTransaction(memberId, payDate, 19.42);
            sct.Execute();
            var sctEarly = new ServiceChargeTransaction(memberId, earlyDate, 100.0);
            sctEarly.Execute();
            var sctLate = new ServiceChargeTransaction(memberId, lateDate, 200.0);
            sctLate.Execute();

            var tct = new TimeCardTransaction(payDate, 8.0, empId);
            tct.Execute();
            var pt = new PaydayTransaction(payDate);
            pt.Execute();

            Paycheck pc = pt.GetPaycheck(empId);
            Assert.NotNull(pc);
            Assert.Equal(payDate, pc.PayPeriodEndDate);
            Assert.Equal(8 * 15.25, pc.GrossPay);
            Assert.Equal("Hold", pc.GetField("Disposition"));
            Assert.Equal(9.42 + 19.42, pc.Deductions);
            Assert.Equal((8 * 15.25) - (9.42 + 19.42), pc.NetPay);
        }

        private static int SetupCommissionedEmployee(
                                                int empId = CommissionedEmployeeId)
        {
            new AddCommissionedEmployee(empId, "Bob", "Home", 1500.0, 2.5)
                .Execute();
            return empId;
        }

        private static int SetupHourlyEmployee(int empId = HourlyEmployeeId)
        {
            new AddHourlyEmployee(empId, "Bill", "Home", 15.25)
                .Execute();
            return empId;
        }

        private static int SetupSalariedEmployee(int empId = SalariedEmployeeId)
        {
            new AddSalariedEmployee(empId, "John", "Lost", 2300.0)
                .Execute();
            return empId;
        }

        private void ValidateNoDeductionPaycheck(PaydayTransaction pt,
                                            int empId,
                                            DateTime payDate,
                                            double pay)
        {
            Paycheck pc = pt.GetPaycheck(empId);
            Assert.NotNull(pc);
            Assert.Equal(payDate, pc.PayPeriodEndDate);
            Assert.Equal(pay, pc.GrossPay);
            Assert.Equal("Hold", pc.GetField("Disposition"));
            Assert.Equal(0.0, pc.Deductions);
            Assert.Equal(pay, pc.NetPay);
        }
    }
}
