using System;

namespace Payroll
{
    public static class DateUtil
    {
        public static bool IsInPayPeriod(DateTime theDate,
                                         DateTime startDate,
                                         DateTime endDate) =>
            (theDate >= startDate) && (theDate <= endDate);
    }
}
