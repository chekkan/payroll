using System;

namespace Payroll
{
    public class UnionAffiliation : Affiliation
    {
        private ServiceCharge serviceCharge;

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