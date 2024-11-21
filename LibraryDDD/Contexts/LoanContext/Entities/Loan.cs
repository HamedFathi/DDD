using HamedStack.TheAggregateRoot;
using HamedStack.TheResult;
using LibraryDDD.Contexts.BookContext.Entities;
using LibraryDDD.Contexts.BookContext.Enumerations;
using LibraryDDD.Contexts.LoanContext.Enumerations;
using LibraryDDD.Contexts.LoanContext.ValueObjects;
using LibraryDDD.Contexts.MemberContext.AggregateRoots;

// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract

namespace LibraryDDD.Contexts.LoanContext.Entities;

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
        return Status == LoanStatus.Active && DateTime.Now > LoanPeriod.EndDate);
    }

    public Result<bool> MarkAsOverdue()
    {
        if (!IsOverdue())
            return Result<bool>.Failure("Loan is not overdue.");

        Status = LoanStatus.Overdue;
        return Result<bool>.Success(true);
    }

    public void ExtendLoanPeriod(DateTime newEndDate)
    {
        LoanPeriod = LoanPeriod.Create(LoanPeriod.StartDate, newEndDate).Value!;
    }
}
