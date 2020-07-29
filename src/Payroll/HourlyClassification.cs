using System;
using System.Collections.Generic;

namespace Payroll
{
    public class HourlyClassification : PaymentClassification
    {
        private readonly IDictionary<DateTime, TimeCard> timeCards;

        public HourlyClassification(double hourlyRate)
        {
            HourlyRate = hourlyRate;
            timeCards = new Dictionary<DateTime, TimeCard>();
        }

        public double HourlyRate { get; }

        public void AddTimeCard(TimeCard timeCard)
        {
            timeCards.Add(timeCard.Date, timeCard);
        }

        public double CalculatePay(Paycheck paycheck)
        {
            double totalPay = 0.0;

            foreach (var timeCard in timeCards.Values)
            {
                if (IsInPayPeriod(timeCard, paycheck.PayDate))
                    totalPay += CalculatePayForTimeCard(timeCard);
            }

            return totalPay;
        }

        public TimeCard GetTimeCard(DateTime dateTime) => timeCards[dateTime];

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