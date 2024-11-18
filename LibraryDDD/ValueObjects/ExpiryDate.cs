using HamedStack.TheResult;

namespace LibraryDDD.ValueObjects;

public class ExpiryDate : DateValueObject
{
    private ExpiryDate(DateTime value) : base(value) { }

    public static Result<ExpiryDate> Create(DateTime reservationDate, int reservationExpiryDays)
    {
        var expiryDate = reservationDate.AddDays(reservationExpiryDays);
        return Result<ExpiryDate>.Success(new ExpiryDate(expiryDate));
    }
}