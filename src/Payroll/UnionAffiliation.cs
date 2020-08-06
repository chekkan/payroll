using System;

namespace Payroll
{
    public class UnionAffiliation : Affiliation
    {
        private ServiceCharge serviceCharge;

        public UnionAffiliation(int memberId, double dues)
        {
            MemberId = memberId;
            Dues = dues;
        }

        public int MemberId { get; }
        public double Dues { get; }

        public void AddServiceCharge(ServiceCharge serviceCharge)
        {
            this.serviceCharge = serviceCharge;
        }

        public double CalculateDeduction(Paycheck paycheck)
        {
            double totalDues = 0;
            int fridays = NumberOfFridaysInPayPeriod(paycheck.PayPeriodStartDate,
                                                     paycheck.PayPeriodEndDate);
            totalDues = Dues * fridays;
            return totalDues;
        }

        private int NumberOfFridaysInPayPeriod(DateTime payPeriodStart,
                                               DateTime payPeriodEnd)
        {
            int fridays = 0;
            for (DateTime day = payPeriodStart;
                 day <= payPeriodEnd;
                 day = day.AddDays(1))
            {
                if (day.DayOfWeek == DayOfWeek.Friday)
                    fridays++;
            }

            return fridays;
        }

        public ServiceCharge GetServiceCharge(DateTime time)
        {
            return serviceCharge;
        }
    }
}
