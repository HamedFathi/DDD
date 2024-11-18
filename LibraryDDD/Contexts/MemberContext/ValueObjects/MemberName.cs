using HamedStack.TheAggregateRoot;
using HamedStack.TheResult;

namespace LibraryDDD.Contexts.MemberContext.ValueObjects;

public class MemberName : ValueObject
{
    public string FirstName { get; }
    public string LastName { get; }

    private MemberName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public static Result<MemberName> Create(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Result<MemberName>.Failure("First name cannot be empty.");

        if (string.IsNullOrWhiteSpace(lastName))
            return Result<MemberName>.Failure("Last name cannot be empty.");

        return Result<MemberName>.Success(new MemberName(firstName, lastName));
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName}";
    }
}