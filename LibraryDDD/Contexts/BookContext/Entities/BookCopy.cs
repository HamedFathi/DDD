using HamedStack.TheAggregateRoot;
using HamedStack.TheResult;
using LibraryDDD.Contexts.BookContext.AggregateRoots;
using LibraryDDD.Contexts.BookContext.Enumerations;
using LibraryDDD.Contexts.BookContext.ValueObjects;

// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract

namespace LibraryDDD.Contexts.BookContext.Entities;

public class BookCopy : Entity<Guid>
{
    public Book Book { get; }
    public Barcode Barcode { get; }
    public CopyCondition Condition { get; private set; }
    public BookCopyStatus CurrentStatus { get; private set; }

    private BookCopy(Book book, Barcode barcode, CopyCondition condition)
    {
        Id = Guid.NewGuid();
        Book = book;
        Barcode = barcode;
        Condition = condition;
        CurrentStatus = BookCopyStatus.Available;
    }

    public static Result<BookCopy> Create(Book book, CopyCondition condition)
    {
        var barcode = Barcode.Create(Guid.NewGuid().ToString());
        return barcode.IsSuccess
            ? Result<BookCopy>.Success(new BookCopy(book, barcode.Value!, condition))
            : Result<BookCopy>.Failure(barcode.Errors);
    }

    public void CheckOut()
    {
        CurrentStatus = BookCopyStatus.Loaned;
    }

    public void CheckIn()
    {
        CurrentStatus = BookCopyStatus.Available;
    }

    public void MarkAsDamaged()
    {
        Condition = CopyCondition.Damaged;
    }
}
