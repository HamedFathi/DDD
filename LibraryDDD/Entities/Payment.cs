using HamedStack.TheAggregateRoot;
using HamedStack.TheResult;
using LibraryDDD.AggregateRoots;
using LibraryDDD.Enumerations;
using LibraryDDD.ValueObjects;

namespace LibraryDDD.Entities;

public class Payment : Entity<Guid>
{
    public Member Member { get; }
    public Money Amount { get; }
    public DateTime Date { get; }
    public PaymentStatus Status { get; private set; }
    public PaymentType PaymentType { get; }

    private Payment(Member member, Money amount, PaymentType paymentType)
    {
        Id = Guid.NewGuid();
        Member = member;
        Amount = amount;
        Date = DateTime.Now;
        Status = PaymentStatus.Pending;
        PaymentType = paymentType;
    }

    public static Result<Payment> Create(Member member, Money amount, PaymentType paymentType)
    {
        return Result<Payment>.Success(new Payment(member, amount, paymentType));
    }

    public void Complete()
    {
        Status = PaymentStatus.Completed;
    }

    public void Cancel()
    {
        Status = PaymentStatus.Cancelled;
    }
}