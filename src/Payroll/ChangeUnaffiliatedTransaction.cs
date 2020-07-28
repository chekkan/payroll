namespace Payroll
{
    public class ChangeUnaffiliatedTransaction : ChangeAffiliationTransaction
    {
        public ChangeUnaffiliatedTransaction(int empId) : base(empId)
        { }

        protected override Affiliation Affiliation =>
            new NoAffiliation();

        protected override void RecordMembership(Employee e)
        {
            if (e.Affiliation is UnionAffiliation affiliation)
                PayrollDatabase.RemoveUnionMember(affiliation.MemberId);
        }
    }
}