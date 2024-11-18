using HamedStack.TheAggregateRoot;
using HamedStack.TheResult;

namespace LibraryDDD.ValueObjects;

public class Authors : ValueObject
{
    private readonly List<Author> _authors;

    public IReadOnlyList<Author> All => _authors.AsReadOnly();

    private Authors(List<Author> authors)
    {
        _authors = authors;
    }

    public static Result<Authors> Create(IEnumerable<string> authorNames)
    {
        var enumerable = authorNames.ToList();
        if (!enumerable.Any())
            return Result<Authors>.Failure("A book must have at least one author.");

        var authorList = new List<Author>();
        var errors = new List<string>();

        foreach (var name in enumerable)
        {
            var authorResult = Author.Create(name);
            if (authorResult.IsSuccess)
                authorList.Add(authorResult.Value!);
            else
                errors.AddRange(authorResult.Errors.Select(err => err.Message));
        }

        return errors.Count > 0
            ? Result<Authors>.Failure(errors.ToArray())
            : Result<Authors>.Success(new Authors(authorList));
    }

    public override string ToString()
    {
        return string.Join(", ", _authors.Select(a => a.Value));
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        foreach (var author in _authors)
            yield return author;
    }
}