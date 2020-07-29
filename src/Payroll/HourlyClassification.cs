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

        public double HourlyRate { get; }

        public void AddTimeCard(TimeCard timeCard)
        {
            this.timeCard = timeCard;
        }

        public double CalculatePay(Paycheck paycheck)
        {
            double totalPay = 0.0;

            if (timeCard != null && IsInPayPeriod(timeCard, paycheck.PayDate))
                totalPay += CalculatePayForTimeCard(timeCard);

            return totalPay;
        }

        public TimeCard GetTimeCard(DateTime dateTime)
        {
            return this.timeCard;
        }

        private bool IsInPayPeriod(TimeCard card, DateTime payPeriod)
        {
            DateTime payPeriodEndDate = payPeriod;
            DateTime payPeriodStartDate = payPeriod.AddDays(-5);

            return card.Date <= payPeriodEndDate && card.Date >= payPeriodStartDate;
        }

        private double CalculatePayForTimeCard(TimeCard card)
        {
            double overtimeHours = Math.Max(0.0, card.Hours - 8);
            double normalHours = card.Hours - overtimeHours;
            return HourlyRate * normalHours + HourlyRate * 1.5 * overtimeHours;
        }
    }
}