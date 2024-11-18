using HamedStack.TheAggregateRoot;
using HamedStack.TheResult;
using LibraryDDD.AggregateRoots;
using LibraryDDD.Enumerations;
using LibraryDDD.ValueObjects;

// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract

namespace LibraryDDD.Entities;

public class Loan : Entity<Guid>
{
    public Member Member { get; }
    public BookCopy Copy { get; }
    public LoanPeriod LoanPeriod { get; private set; }
    public DateTime? ReturnDate { get; private set; }
    public LoanStatus Status { get; private set; }

    private Loan(Member member, BookCopy copy, LoanPeriod loanPeriod)
    {
        Id = Guid.NewGuid();
        Member = member;
        Copy = copy;
        LoanPeriod = loanPeriod;
        Status = LoanStatus.Active;
    }

    public static Result<Loan> Create(Member member, BookCopy copy, LoanPeriod loanPeriod)
    {
        if (member == null)
            return Result<Loan>.Failure("Member cannot be null.");

        if (copy == null || copy.CurrentStatus != BookCopyStatus.Available)
            return Result<Loan>.Failure("Book copy is not available.");

        return Result<Loan>.Success(new Loan(member, copy, loanPeriod));
    }

    public Result<bool> CanReturn()
    {
        if (Status != LoanStatus.Active)
            return Result<bool>.Failure("Loan is not active and cannot be returned.");

        return Result<bool>.Success(true);
    }

    public void Return()
    {
        ReturnDate = DateTime.Now;
        Status = LoanStatus.Closed;
        Copy.CheckIn();
    }

    public bool IsOverdue()
    {
        if (Status == LoanStatus.Active && DateTime.Now > LoanPeriod.EndDate)
        {
            Status = LoanStatus.Overdue;
            return true;
        }
        return false;
    }

    public void ExtendLoanPeriod(DateTime newEndDate)
    {
        LoanPeriod = LoanPeriod.Create(LoanPeriod.StartDate, newEndDate).Value!;
    }
}
