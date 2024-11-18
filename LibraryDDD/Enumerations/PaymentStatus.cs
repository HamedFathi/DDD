using Ardalis.SmartEnum;

namespace LibraryDDD.Enumerations;

public class PaymentStatus(string name, int value) : SmartEnum<PaymentStatus>(name, value)
{
    public static readonly PaymentStatus Pending = new PaymentStatus(nameof(Pending), 1);
    public static readonly PaymentStatus Completed = new PaymentStatus(nameof(Completed), 2);
    public static readonly PaymentStatus Cancelled = new PaymentStatus(nameof(Cancelled), 3);
}