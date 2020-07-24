using System;

namespace Payroll
{
    public class HourlyClassification : PaymentClassification
    {
        private TimeCard timeCard;

        public HourlyClassification(double hourlyRate)
        {
            HourlyRate = hourlyRate;
        }

        public double HourlyRate { get; private set; }

        public void AddTimeCard(TimeCard timeCard)
        {
            this.timeCard = timeCard;
        }

        public TimeCard GetTimeCard(DateTime dateTime)
        {
            return this.timeCard;
        }
    }
}