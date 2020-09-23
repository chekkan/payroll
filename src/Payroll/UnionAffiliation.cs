using System;
using System.Collections.Generic;
using System.Linq;

namespace Payroll
{
    public class UnionAffiliation : Affiliation
    {
        private readonly IDictionary<DateTime, ServiceCharge> serviceCharges;

        public UnionAffiliation(int memberId, double dues)
        {
            MemberId = memberId;
            Dues = dues;
            serviceCharges = new Dictionary<DateTime, ServiceCharge>();
        }

        public int MemberId { get; }
        public double Dues { get; }

        public void AddServiceCharge(ServiceCharge serviceCharge)
        {
            serviceCharges.Add(serviceCharge.Time, serviceCharge);
        }

        public ServiceCharge GetServiceCharge(DateTime time) => serviceCharges[time];

        public double CalculateDeduction(Paycheck paycheck)
        {
            int fridays = NumberOfFridaysInPayPeriod(paycheck.PayPeriodStartDate,
                                                     paycheck.PayPeriodEndDate);
            double totalDues = Dues * fridays;

            double serviceCharge = ServiceChargeForPeriod(paycheck.PayPeriodStartDate,
                                                             paycheck.PayPeriodEndDate);

            return totalDues + serviceCharge;
        }

        private static int NumberOfFridaysInPayPeriod(DateTime payPeriodStart,
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

        private double ServiceChargeForPeriod(DateTime startDate, DateTime endDate) =>
            serviceCharges
                .Where(charge => charge.Key >= startDate && charge.Key <= endDate)
                .Sum(charge => charge.Value.Amount);
    }
}
