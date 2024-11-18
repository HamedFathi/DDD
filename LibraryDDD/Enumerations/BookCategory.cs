using Ardalis.SmartEnum;

namespace LibraryDDD.Enumerations;

public class BookCategory(string name, int value) : SmartEnum<BookCategory>(name, value)
{
    public static readonly BookCategory Fiction = new BookCategory(nameof(Fiction), 1);
    public static readonly BookCategory NonFiction = new BookCategory(nameof(NonFiction), 2);
    public static readonly BookCategory Mystery = new BookCategory(nameof(Mystery), 3);
    public static readonly BookCategory Fantasy = new BookCategory(nameof(Fantasy), 4);
    public static readonly BookCategory ScienceFiction = new BookCategory(nameof(ScienceFiction), 5);
    public static readonly BookCategory Biography = new BookCategory(nameof(Biography), 6);
    public static readonly BookCategory Romance = new BookCategory(nameof(Romance), 7);
    public static readonly BookCategory Horror = new BookCategory(nameof(Horror), 8);
    public static readonly BookCategory Thriller = new BookCategory(nameof(Thriller), 9);
    public static readonly BookCategory Historical = new BookCategory(nameof(Historical), 10);
    public static readonly BookCategory Adventure = new BookCategory(nameof(Adventure), 11);
    public static readonly BookCategory Children = new BookCategory(nameof(Children), 12);
    public static readonly BookCategory YoungAdult = new BookCategory(nameof(YoungAdult), 13);
    public static readonly BookCategory SelfHelp = new BookCategory(nameof(SelfHelp), 14);
    public static readonly BookCategory GraphicNovel = new BookCategory(nameof(GraphicNovel), 15);
    public static readonly BookCategory Poetry = new BookCategory(nameof(Poetry), 16);
    public static readonly BookCategory Dystopian = new BookCategory(nameof(Dystopian), 17);
    public static readonly BookCategory Contemporary = new BookCategory(nameof(Contemporary), 18);
    public static readonly BookCategory Science = new BookCategory(nameof(Science), 19);
    public static readonly BookCategory Philosophy = new BookCategory(nameof(Philosophy), 20);
}