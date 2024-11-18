using Ardalis.SmartEnum;

namespace LibraryDDD.Contexts.ReservationContext.Enumerations;

public class ReservationStatus(string name, int value) : SmartEnum<ReservationStatus>(name, value)
{
    public static readonly ReservationStatus Pending = new(nameof(Pending), 1);
    public static readonly ReservationStatus Fulfilled = new(nameof(Fulfilled), 2);
    public static readonly ReservationStatus Cancelled = new(nameof(Cancelled), 3);

    public bool CanBeCancelled()
    {
        return this == Pending;
    }
}