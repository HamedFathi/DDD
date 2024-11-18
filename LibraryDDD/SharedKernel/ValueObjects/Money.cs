using HamedStack.TheAggregateRoot;
using HamedStack.TheResult;
using LibraryDDD.SharedKernel.Enumerations;

// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract 
 
namespace LibraryDDD.SharedKernel.ValueObjects;

public sealed class Money : ValueObject
{
    public decimal Amount { get; }
    public Currency Currency { get; }

    private Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Result<Money> Create(decimal amount, Currency currency)
    {
        if (currency == null)
            return Result<Money>.Failure("Currency must not be null.");

        if (amount < 0)
            return Result<Money>.Failure("Amount cannot be negative.");

        return Result<Money>.Success(new Money(amount, currency));
    }

    public Result<Money> Add(Money other)
    {
        if (other is null)
            return Result<Money>.Failure("Cannot add a null Money value.");

        if (!Currency.Equals(other.Currency))
            return Result<Money>.Failure($"Currency mismatch: Cannot add {Currency.Name} to {other.Currency.Name}.");

        return Create(Amount + other.Amount, Currency);
    }

    public Result<Money> Subtract(Money other)
    {
        if (other is null)
            return Result<Money>.Failure("Cannot subtract a null Money value.");

        if (!Currency.Equals(other.Currency))
            return Result<Money>.Failure($"Currency mismatch: Cannot subtract {other.Currency.Name} from {Currency.Name}.");

        return Create(Amount - other.Amount, Currency);
    }

    public Result<Money> Multiply(decimal factor)
    {
        if (factor < 0)
            return Result<Money>.Failure("Factor cannot be negative.");

        return Create(Amount * factor, Currency);
    }

    public Result<Money> Divide(decimal divisor)
    {
        if (divisor <= 0)
            return Result<Money>.Failure("Divisor must be greater than zero.");

        return Create(Amount / divisor, Currency);
    }

    public override string ToString() => $"{Currency.Symbol}{Amount:N2} {Currency.ISOCode}";

    public static Money Zero(Currency currency) => new(0, currency);
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency.Name;
    }
}