using HamedStack.TheAggregateRoot;
using HamedStack.TheResult;

namespace LibraryDDD.Contexts.MemberContext.ValueObjects;

public class Address : ValueObject
{
    public string Street { get; }
    public string City { get; }
    public string State { get; }
    public string PostalCode { get; }
    public string Country { get; }

    private Address(string street, string city, string state, string postalCode, string country)
    {
        Street = street;
        City = city;
        State = state;
        PostalCode = postalCode;
        Country = country;
    }

    public static Result<Address> Create(string street, string city, string state, string postalCode, string country)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(street))
            errors.Add("Street cannot be empty.");

        if (string.IsNullOrWhiteSpace(city))
            errors.Add("City cannot be empty.");

        if (string.IsNullOrWhiteSpace(postalCode))
            errors.Add("Postal Code cannot be empty.");

        if (string.IsNullOrWhiteSpace(country))
            errors.Add("Country cannot be empty.");

        return errors.Count > 0
            ? Result<Address>.Failure(errors.ToArray())
            : Result<Address>.Success(new Address(street, city, state, postalCode, country));
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
        yield return State;
        yield return PostalCode;
        yield return Country;
    }
}