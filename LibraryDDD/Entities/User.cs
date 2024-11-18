using System.Security.Cryptography;
using System.Text;
using HamedStack.TheAggregateRoot;
using HamedStack.TheResult;
using LibraryDDD.AggregateRoots;
using LibraryDDD.Enumerations;

namespace LibraryDDD.Entities;

public class User : Entity<Guid>
{
    public string Username { get; private set; }
    public string PasswordHash { get; private set; }
    public UserRole Role { get; private set; }
    public string Email { get; private set; }
    public string FullName { get; private set; }

    public Member? Member { get; private set; }

    private User(string username, string passwordHash, UserRole role, string email, string fullName)
    {
        Id = Guid.NewGuid();
        Username = username;
        PasswordHash = passwordHash;
        Role = role;
        Email = email;
        FullName = fullName;
    }

    public static Result<User> Create(string username, string password, UserRole role, string email, string fullName)
    {
        if (string.IsNullOrWhiteSpace(username))
            return Result<User>.Failure("Username is required.");

        if (string.IsNullOrWhiteSpace(password))
            return Result<User>.Failure("Password is required.");

        if (string.IsNullOrWhiteSpace(email))
            return Result<User>.Failure("Email is required.");

        var passwordHash = HashPassword(password);

        return Result<User>.Success(new User(username, passwordHash, role, email, fullName));
    }

    private static string HashPassword(string password)
    {
        using var sha256Hash = SHA256.Create();
        var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
        var builder = new StringBuilder();
        foreach (var t in bytes)
        {
            builder.Append(t.ToString("x2"));
        }
        return builder.ToString();
    }

    public Result<bool> LinkToMember(Member member)
    {
        if (Member != null)
            return Result<bool>.Failure(false, "User is already linked to a member.");

        Member = member;
        return new Result<bool>(true);
    }
}