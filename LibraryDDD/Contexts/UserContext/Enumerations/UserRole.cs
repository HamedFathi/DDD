using Ardalis.SmartEnum;

namespace LibraryDDD.Contexts.UserContext.Enumerations;

public class UserRole(string name, int value) : SmartEnum<UserRole>(name, value)
{
    public static readonly UserRole Admin = new(nameof(Admin), 1);
    public static readonly UserRole Librarian = new(nameof(Librarian), 2);
    public static readonly UserRole RegularUser = new(nameof(RegularUser), 3);
}