using HamedStack.TheAggregateRoot;
using HamedStack.TheResult;

namespace LibraryDDD.ValueObjects;

public class LoanPeriod : ValueObject
{
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }

    private LoanPeriod(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }

    public static Result<LoanPeriod> Create(DateTime startDate, DateTime endDate)
    {
        if (startDate >= endDate)
            return Result<LoanPeriod>.Failure("Start date must be earlier than end date.");

        return Result<LoanPeriod>.Success(new LoanPeriod(startDate, endDate));
    }

    public int GetDurationInDays()
    {
        return (EndDate - StartDate).Days;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return StartDate;
        yield return EndDate;
    }
}