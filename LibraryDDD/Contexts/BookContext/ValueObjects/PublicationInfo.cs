using HamedStack.TheAggregateRoot;
using HamedStack.TheResult;

namespace LibraryDDD.Contexts.BookContext.ValueObjects;

public class PublicationInfo : ValueObject
{
    public string Publisher { get; }
    public int PublicationYear { get; }
    public string Edition { get; }

    private PublicationInfo(string publisher, int publicationYear, string edition)
    {
        Publisher = publisher;
        PublicationYear = publicationYear;
        Edition = edition;
    }

    public static Result<PublicationInfo> Create(string publisher, int publicationYear, string edition = "")
    {
        var errors = new List<string>();
        if (string.IsNullOrWhiteSpace(publisher))
            errors.Add("Publisher cannot be empty.");

        if (publicationYear < 0 || publicationYear > DateTime.Now.Year)
            errors.Add("Publication year is invalid.");

        return errors.Count > 0
            ? Result<PublicationInfo>.Failure(errors.ToArray())
            : Result<PublicationInfo>.Success(new PublicationInfo(publisher, publicationYear, edition));
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Publisher;
        yield return PublicationYear;
        yield return Edition;
    }
}