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

        public ServiceCharge GetServiceCharge(DateTime time)
        {
            return serviceCharge;
        }
    }
}