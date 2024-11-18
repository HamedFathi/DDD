using Ardalis.SmartEnum;

namespace LibraryDDD.Contexts.BookContext.Enumerations;

public sealed class CopyCondition(string name, int value) : SmartEnum<CopyCondition>(name, value)
{
    public static readonly CopyCondition New = new(nameof(New), 1);
    public static readonly CopyCondition Usable = new(nameof(Usable), 2);
    public static readonly CopyCondition Damaged = new(nameof(Damaged), 3);
}