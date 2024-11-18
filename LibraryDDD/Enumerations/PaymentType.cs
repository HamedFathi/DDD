using Ardalis.SmartEnum;

namespace LibraryDDD.Enumerations;

public class PaymentType(string name, int value) : SmartEnum<PaymentType>(name, value)
{
    public static readonly PaymentType FinePayment = new PaymentType(nameof(FinePayment), 1);
    public static readonly PaymentType MembershipFee = new PaymentType(nameof(MembershipFee), 2);
    public static readonly PaymentType Other = new PaymentType(nameof(Other), 3);
}