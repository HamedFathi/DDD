using Ardalis.SmartEnum;

namespace LibraryDDD.Enumerations;

public class MembershipStatus(string name, int value) : SmartEnum<MembershipStatus>(name, value)
{
    public static readonly MembershipStatus Active = new(nameof(Active), 1);
    public static readonly MembershipStatus Inactive = new(nameof(Inactive), 2);
    public static readonly MembershipStatus Suspended = new(nameof(Suspended), 3);

    public bool CanBeReactivated()
    {
        return this == Suspended;
    }
}