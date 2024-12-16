// (c) Yury Malich, 2024-2025
// MIT License

using System;
using System.Diagnostics.Contracts;
using System.Globalization;

namespace ValueStringType;

/// <summary>
/// ValueString extension methods
/// </summary>
public static class ValueStringExt
{

    public static ReadOnlySpan<char> AsSpan(this ValueString text)
    {
        return text.Value.AsSpan();
    }

    /// <summary>
    /// Extension method returns string casted to ValueString.
    /// </summary>
    /// <param name="text">text to cast to ValueString</param>
    /// <returns>ValueString representation</returns>
    public static ValueString ToValueString(this string? text)
    {
        return new ValueString(text);
    }

    /// <summary>
    /// Retrieves a given string if it has any characters or null, if the given instance is empty.
    /// </summary>
    [Pure]
    public static string? NullIfEmpty(this ValueString value)
    {
        return value.IsNotEmpty ? value.ToString() : null;
    }

    /// <summary>
    /// Retrieves a given string if it has any characters or specified default string, if the given instance is empty.
    /// </summary>
    [Pure]
    public static ValueString NotEmptyOr(this ValueString value, ValueString defaultValue)
    {
        return value.IsNotEmpty ? value : defaultValue;
    }

    /// <summary>
    /// Tries to parse a 32-bit integer value from a string.
    /// </summary>
    /// <returns>@default, if the parsing fails, otherwise the value</returns>
    public static int? TryParseInt(this ValueString value)
    {
        return int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var number) ? number : null;
    }

    /// <summary>
    /// Tries to parse a 64-bit integer value from a string.
    /// </summary>
    /// <returns>null, if the parsing fails, otherwise the value</returns>
    public static long? TryParseLong(this ValueString value)
    {
        return long.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var number) ? number : null;
    }

    /// <summary>
    /// Tries to parse a double value from a string using invariant culture .
    /// </summary>
    /// <returns>null, if the parsing fails, otherwise the value</returns>
    public static double? TryParseDouble(this ValueString value)
    {
        return value.TryParseDouble(CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Tries to parse a double value from a string using given culture info.
    /// </summary>
    /// <returns>null, if the parsing fails, otherwise the value</returns>
    public static double? TryParseDouble(this ValueString value, IFormatProvider cultureInfo)
    {
        return double.TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, cultureInfo, out var number) ? number : null;
    }

    /// <summary>
    /// Tries to parse a decimal value from a string using invariant culture.
    /// </summary>
    /// <returns>@default, if the parsing fails, otherwise the value</returns>
    public static decimal? TryParseDecimal(this ValueString value)
    {
        return value.TryParseDecimal(CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Tries to parse a decimal value from a string using given culture info.
    /// </summary>
    /// <returns>@default, if the parsing fails, otherwise the value</returns>
    public static decimal? TryParseDecimal(this ValueString value, IFormatProvider cultureInfo)
    {
        return decimal.TryParse(value, NumberStyles.Any, cultureInfo, out var number) ? number : null;
    }

    /// <summary>
    /// Tries to parse a DateTime value from a string.
    /// </summary>
    /// <returns>null, if the parsing fails, otherwise the value</returns>
    public static DateTime? TryParseDateTime(this ValueString value, DateTime? @default = null)
    {
        return DateTime.TryParse(value, out DateTime dt) ? dt : @default;
    }

    /// <summary>Tries to parse a DateTime value from a string.</summary>
    /// <param name="format">The required format of string. See the Remarks section for more information. </param>
    /// <param name="style">A bitwise combination of one or more enumeration values that indicate the permitted format of string.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information about string.</param>
    /// <returns>null, if the parsing fails, otherwise the value</returns>
    public static DateTime? TryParseDateTimeExact(this ValueString value, string format, DateTimeStyles style = DateTimeStyles.None, IFormatProvider? provider = null)
    {
        return DateTime.TryParseExact(value, format, provider, style, out DateTime dt) ? dt : null;
    }

    /// <summary>Tries to parse a DateTime value from a string.</summary>
    /// <param name="formats">The required format of string. See the Remarks section for more information. </param>
    /// <param name="style">A bitwise combination of one or more enumeration values that indicate the permitted format of string.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information about string.</param>
    /// <returns>null, if the parsing fails, otherwise the value</returns>
    public static DateTime? TryParseDateTimeExact(this ValueString value, string[] formats, DateTimeStyles style = DateTimeStyles.None, IFormatProvider? provider = null)
    {
        return DateTime.TryParseExact(value, formats, provider, style, out DateTime dt) ? dt : null;
    }

    /// <summary>
    /// Tries to parse a 32-bit integer value from a string.
    /// </summary>
    /// <returns>@default, if the parsing fails, otherwise the value</returns>
    public static long TryParseInt(this ValueString value, long @default)
    {
        return value.TryParseInt() ?? @default;
    }

    /// <summary>
    /// Tries to parse a 64-bit integer value from a string.
    /// </summary>
    /// <returns>@default, if the parsing fails, otherwise the value</returns>
    public static long TryParseLong(this ValueString value, long @default)
    {
        return value.TryParseLong() ?? @default;
    }

    /// <summary>
    /// Tries to parse a double value from a string using invariant culture info.
    /// </summary>
    /// <returns>@default, if the parsing fails, otherwise the value</returns>
    public static double TryParseDouble(this ValueString value, double @default)
    {
        return value.TryParseDouble() ?? @default;
    }

    /// <summary>
    /// Tries to parse a double value from a string using given culture info.
    /// </summary>
    /// <returns>@default, if the parsing fails, otherwise the value</returns>
    public static double TryParseDouble(this ValueString value, IFormatProvider cultureInfo, double @default)
    {
        return value.TryParseDouble(cultureInfo) ?? @default;
    }

    /// <summary>
    /// Tries to parse a decimal value from a string.
    /// </summary>
    /// <returns>@default, if the parsing fails, otherwise the value</returns>
    public static decimal TryParseDecimal(this ValueString value, decimal @default)
    {
        return value.TryParseDecimal() ?? @default;
    }

    /// <summary>
    /// Tries to parse a decimal value from a string.
    /// </summary>
    /// <returns>@default, if the parsing fails, otherwise the value</returns>
    public static decimal TryParseDecimal(this ValueString value, IFormatProvider cultureInfo, decimal @default)
    {
        return value.TryParseDecimal(cultureInfo) ?? @default;
    }

    public static string Format(this ValueString format, object? arg0)
    {
        return string.Format(format, arg0);
    }

    public static string Format(this ValueString format, object? arg0, object? arg1)
    {
        return string.Format(format, arg0, arg1);
    }

    public static string Format(this ValueString format, object? arg0, object? arg1, object? arg2)
    {
        return string.Format(format, arg0, arg1, arg2);
    }

    public static string Format(this ValueString format, object? arg0, object? arg1, object? arg2, object? arg3)
    {
        return string.Format(format, arg0, arg1, arg2, arg3);
    }
}