using Ardalis.SmartEnum;

namespace LibraryDDD.Enumerations;

public sealed class LoanStatus(string name, int value) : SmartEnum<LoanStatus>(name, value)
{
    public static readonly LoanStatus Active = new(nameof(Active), 1);
    public static readonly LoanStatus Closed = new(nameof(Closed), 2);
    public static readonly LoanStatus Overdue = new(nameof(Overdue), 3);

    public bool IsDelinquent()
    {
        return this == Overdue;
    }

    public LoanStatus GetNextStatus()
    {
        return this switch
        {
            _ when this == Active => Closed,
            _ when this == Closed => Closed,
            _ when this == Overdue => Active,
            _ => Active
        };
    }
}