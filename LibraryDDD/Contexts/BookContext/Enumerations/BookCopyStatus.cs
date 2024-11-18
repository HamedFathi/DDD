using Ardalis.SmartEnum;

namespace LibraryDDD.Contexts.BookContext.Enumerations;

public sealed class BookCopyStatus(string name, int value) : SmartEnum<BookCopyStatus>(name, value)
{
    public static readonly BookCopyStatus Available = new(nameof(Available), 1);
    public static readonly BookCopyStatus Loaned = new(nameof(Loaned), 2);
    public static readonly BookCopyStatus Reserved = new(nameof(Reserved), 3);

    public bool CanTransitionTo(BookCopyStatus newStatus)
    {
        return this switch
        {
            _ when this == Available && (newStatus == Reserved || newStatus == Loaned) => true,
            _ when this == Reserved && newStatus == Loaned => true,
            _ when this == Loaned && newStatus == Available => true,
            _ => false
        };
    }
}