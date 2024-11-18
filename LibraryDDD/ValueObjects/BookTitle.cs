using HamedStack.TheAggregateRoot;
using HamedStack.TheResult;

namespace LibraryDDD.ValueObjects;

public class BookTitle : SingleValueObject<string>
{
    private BookTitle(string value) : base(value) { }

    public static Result<BookTitle> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<BookTitle>.Failure("Book title cannot be empty.");
        return Result<BookTitle>.Success(new BookTitle(value));
    }
}