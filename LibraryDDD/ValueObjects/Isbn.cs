using HamedStack.TheAggregateRoot;
using HamedStack.TheResult;

namespace LibraryDDD.ValueObjects;

public class Isbn : SingleValueObject<string>
{
    private Isbn(string value)
    {
        Value = value;
    }

    public static Result<Isbn> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<Isbn>.Failure("ISBN cannot be empty.");

        if (!IsValidIsbn13(value))
            return Result<Isbn>.Failure("ISBN is invalid.");

        return Result<Isbn>.Success(new Isbn(value));
    }

    private static bool IsValidIsbn13(string isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn))
            return false;

        isbn = isbn.Replace("-", "").Replace(" ", "");

        if (isbn.Length != 13 || !long.TryParse(isbn, out _))
        {
            return false;
        }

        var sum = 0;
        for (var i = 0; i < 12; i++)
        {
            var digit = isbn[i] - '0';
            sum += (i % 2 == 0) ? digit : digit * 3;
        }

        var checkDigit = (10 - (sum % 10)) % 10;

        return checkDigit == (isbn[12] - '0');
    }
}