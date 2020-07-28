namespace Payroll
{
    public class NoAffiliation : Affiliation
    {
        public double CalculateDeduction(Paycheck paycheck) => 0.0;
    }
}