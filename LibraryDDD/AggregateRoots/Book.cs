﻿using HamedStack.TheAggregateRoot;
using HamedStack.TheAggregateRoot.Abstractions;
using HamedStack.TheResult;
using LibraryDDD.Entities;
using LibraryDDD.Enumerations;
using LibraryDDD.ValueObjects;

namespace LibraryDDD.AggregateRoots;

public class Book : Entity<Guid>, IAggregateRoot
{
    public BookTitle Title { get; }
    public Authors Authors { get; }
    public Isbn Isbn { get; }
    public PublicationInfo PublicationInfo { get; }
    public BookCategory Category { get; }
    public int MaxCopies { get; private set; }

    private readonly List<BookCopy> _copies = new();
    public IReadOnlyList<BookCopy> Copies => _copies.AsReadOnly();

    private Book(BookTitle title, Authors authors, Isbn isbn, PublicationInfo publicationInfo, BookCategory category, int maxCopies)
    {
        Id = Guid.NewGuid();
        Title = title;
        Authors = authors;
        Isbn = isbn;
        PublicationInfo = publicationInfo;
        Category = category;
        MaxCopies = maxCopies;
    }

    public static Result<Book> Create(string title, IEnumerable<string> authors, Isbn isbn, string publisher, int publicationYear, BookCategory category, int maxCopies, string edition = "")
    {
        var titleResult = BookTitle.Create(title);
        if (!titleResult.IsSuccess)
            return Result<Book>.Failure(titleResult.Errors);

        var authorsResult = Authors.Create(authors);
        if (!authorsResult.IsSuccess)
            return Result<Book>.Failure(authorsResult.Errors);

        var publicationInfoResult = PublicationInfo.Create(publisher, publicationYear, edition);
        if (!publicationInfoResult.IsSuccess)
            return Result<Book>.Failure(publicationInfoResult.Errors);

        return Result<Book>.Success(new Book(titleResult.Value!, authorsResult.Value!, isbn, publicationInfoResult.Value!, category, maxCopies));
    }

    public Result<bool> AddCopy(CopyCondition condition)
    {
        if (_copies.Count >= MaxCopies)
            return Result<bool>.Failure(false, "Cannot add more copies. Maximum limit reached.");

        var bookCopy = BookCopy.Create(this, condition);
        if (bookCopy.IsSuccess)
        {
            _copies.Add(bookCopy.Value!);
            return Result<bool>.Success(true);
        }
        return Result<bool>.Failure(false, bookCopy.Errors);
    }

    public Result<bool> RemoveCopy(BookCopy copy)
    {
        var result = _copies.Remove(copy);
        return new Result<bool>(result);
    }

    public List<BookCopy> GetAvailableCopies()
    {
        return _copies.FindAll(c => c.CurrentStatus == BookCopyStatus.Available);
    }

    public Result<bool> IncreaseMaxCopies(int additionalCopies)
    {
        if (additionalCopies <= 0)
        {
            return Result<bool>.Success(false, "Number of additional copies must be greater than zero.");
        }

        MaxCopies += additionalCopies;
        return Result<bool>.Success(true);

    }

    public override string ToString()
    {
        return $"{Title.Value} by {Authors}";
    }
}