using HamedStack.TheAggregateRoot;
using HamedStack.TheResult;

namespace LibraryDDD.Contexts.BookContext.ValueObjects;

public class Barcode : SingleValueObject<string>
{
    private Barcode(string value) : base(value) { }

    public static Result<Barcode> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<Barcode>.Failure("Barcode cannot be empty.");
        if (value.Length < 5 || value.Length > 15)
            return Result<Barcode>.Failure("Barcode must be between 5 and 15 characters.");
        return Result<Barcode>.Success(new Barcode(value));
    }
}