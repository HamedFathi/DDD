using System.Text.RegularExpressions;
using HamedStack.TheAggregateRoot;
using HamedStack.TheResult;

namespace LibraryDDD.Contexts.MemberContext.ValueObjects;

public class ContactInfo : ValueObject
{
    private static readonly Regex EmailRegex
        = new(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", RegexOptions.Compiled);
    private static readonly Regex PhoneRegex
        = new(@"^\+?[1-9]\d{1,14}$", RegexOptions.Compiled);

    public string Email { get; }
    public string Phone { get; }

    private ContactInfo(string email, string phone)
    {
        Email = email;
        Phone = phone;
    }

    public static Result<ContactInfo> Create(string email, string phone)
    {
        if (string.IsNullOrWhiteSpace(email))
            return Result<ContactInfo>.Failure("Email cannot be empty.");

        if (!IsValidEmail(email))
            return Result<ContactInfo>.Failure("Email format is invalid.");

        if (string.IsNullOrWhiteSpace(phone))
            return Result<ContactInfo>.Failure("Phone number cannot be empty.");

        if (!IsValidPhoneNumber(phone))
            return Result<ContactInfo>.Failure("Phone number format is invalid.");

        return Result<ContactInfo>.Success(new ContactInfo(email, phone));
    }

    private static bool IsValidEmail(string email)
    {
        return EmailRegex.IsMatch(email);
    }

    private static bool IsValidPhoneNumber(string phone)
    {
        return PhoneRegex.IsMatch(phone);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Email;
        yield return Phone;
    }
}