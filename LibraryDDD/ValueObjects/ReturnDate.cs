using HamedStack.TheResult;

namespace LibraryDDD.ValueObjects;

public class ReturnDate : DateValueObject
{
    private ReturnDate(DateTime value) : base(value) { }

    public static Result<ReturnDate> Create(DateTime value)
    {
        if (value > DateTime.Now)
            return Result<ReturnDate>.Failure("Return date cannot be in the future.");
        return Result<ReturnDate>.Success(new ReturnDate(value));
    }
}