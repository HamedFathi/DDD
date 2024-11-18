using HamedStack.TheResult;
using LibraryDDD.SharedKernel.ValueObjects;

namespace LibraryDDD.Contexts.ReservationContext.ValueObjects;

public class ReservationDate : DateValueObject
{
    private ReservationDate(DateTime value) : base(value) { }

    public static Result<ReservationDate> Create(DateTime value)
    {
        return Result<ReservationDate>.Success(new ReservationDate(value));
    }
}