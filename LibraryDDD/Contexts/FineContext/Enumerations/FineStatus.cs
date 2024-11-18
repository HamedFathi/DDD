using Ardalis.SmartEnum;

namespace LibraryDDD.Contexts.FineContext.Enumerations;

public sealed class FineStatus(string name, int value) : SmartEnum<FineStatus>(name, value)
{
    public static readonly FineStatus Unpaid = new(nameof(Unpaid), 1);
    public static readonly FineStatus Paid = new(nameof(Paid), 2);
}