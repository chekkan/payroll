using System;

namespace Payroll
{
    public class DateUtil
    {
        public static bool IsInPayPeriod(DateTime theDate,
                                         DateTime startDate,
                                         DateTime endDate) =>
            (theDate >= startDate) && (theDate <= endDate);
    }
}
