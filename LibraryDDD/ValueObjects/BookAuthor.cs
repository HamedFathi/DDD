using HamedStack.TheAggregateRoot;
using HamedStack.TheResult;

namespace LibraryDDD.ValueObjects;

public class BookAuthor : SingleValueObject<string>
{
    private BookAuthor(string value) : base(value) { }

    public static Result<BookAuthor> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<BookAuthor>.Failure("Author name cannot be empty.");
        return Result<BookAuthor>.Success(new BookAuthor(value));
    }
}