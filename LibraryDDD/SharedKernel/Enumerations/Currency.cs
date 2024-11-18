using Ardalis.SmartEnum;
using HamedStack.TheResult;

// ReSharper disable StringLiteralTypo
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
namespace LibraryDDD.SharedKernel.Enumerations;

public class Currency : SmartEnum<Currency>
{
    public static readonly Currency USD = new("US Dollar", 1, "$", "USD");
    public static readonly Currency EUR = new("Euro", 2, "€", "EUR");
    public static readonly Currency GBP = new("British Pound", 3, "£", "GBP");
    public static readonly Currency JPY = new("Japanese Yen", 4, "¥", "JPY");
    public static readonly Currency AUD = new("Australian Dollar", 5, "A$", "AUD");
    public static readonly Currency CAD = new("Canadian Dollar", 6, "C$", "CAD");
    public static readonly Currency CHF = new("Swiss Franc", 7, "CHF", "CHF");

    public string Symbol { get; }
    public string ISOCode { get; }

    private Currency(string name, int value, string symbol, string isoCode)
        : base(name, value)
    {
        Symbol = symbol;
        ISOCode = isoCode;
    }

    public override string ToString() => $"{Name} ({ISOCode}) - {Symbol}";

    public static Result<Currency> FromISOCode(string isoCode)
    {
        if (string.IsNullOrWhiteSpace(isoCode))
            return Result<Currency>.Failure("ISO code must not be null or empty.");

        var currency = List.FirstOrDefault(c => c.ISOCode.Equals(isoCode, StringComparison.OrdinalIgnoreCase));

        return currency != null
            ? Result<Currency>.Success(currency)
            : Result<Currency>.Failure($"No currency found for ISO code: {isoCode}");
    }

    public static Result<Currency> FromSymbol(string symbol)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            return Result<Currency>.Failure("Symbol must not be null or empty.");

        var currency = List.FirstOrDefault(c => c.Symbol == symbol);

        return currency != null
            ? Result<Currency>.Success(currency)
            : Result<Currency>.Failure($"No currency found for Symbol: {symbol}");
    }
}

