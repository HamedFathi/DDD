using HamedStack.TheAggregateRoot;
using HamedStack.TheResult;

namespace LibraryDDD.Contexts.MemberContext.ValueObjects;

public class MembershipDuration : ValueObject
{
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }

    private MembershipDuration(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }

    public static Result<MembershipDuration> Create(DateTime startDate, DateTime endDate)
    {
        var errors = new List<string>();

        if (startDate == endDate)
            errors.Add("Start date and end date cannot be the same.");

        if (startDate > endDate)
            errors.Add("Start date cannot be later than the end date.");

        return errors.Count > 0
            ? Result<MembershipDuration>.Failure(errors.ToArray())
            : Result<MembershipDuration>.Success(new MembershipDuration(startDate, endDate));
    }

    public bool IsDateWithinRange(DateTime date)
    {
        return date >= StartDate && date <= EndDate;
    }

    public bool IsExpired()
    {
        return DateTime.Now > EndDate;
    }

    public override string ToString()
    {
        return $"{StartDate.ToShortDateString()} - {EndDate.ToShortDateString()}";
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return StartDate;
        yield return EndDate;
    }
}