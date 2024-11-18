using Ardalis.SmartEnum;

namespace LibraryDDD.Contexts.MemberContext.Enumerations;

public abstract class MembershipType(string name, int value) : SmartEnum<MembershipType>(name, value)
{
    public static readonly MembershipType Regular = new RegularMembershipType();
    public static readonly MembershipType Premium = new PremiumMembershipType();

    public abstract int MaxLoans { get; }
    public abstract int MaxReservations { get; }
    public abstract int ReservationExpiryDays { get; }

    private sealed class RegularMembershipType : MembershipType
    {
        public RegularMembershipType() : base("Regular", 1) { }

        public override int ReservationExpiryDays => 30;
        public override int MaxLoans => 3;
        public override int MaxReservations => 1;
    }

    private sealed class PremiumMembershipType : MembershipType
    {
        public PremiumMembershipType() : base("Premium", 2) { }

        public override int ReservationExpiryDays => 60;
        public override int MaxLoans => 5;
        public override int MaxReservations => 2;
    }
}