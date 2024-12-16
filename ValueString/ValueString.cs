// (c) Yury Malich, 2024-2025
// MIT License

using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ValueStringType;

/// <summary>
/// Non-nullable value string.
/// Represents text as a sequence of UTF-16 code units.
/// </summary>
[DebuggerDisplay("{Value}")]
[DataContract(Namespace = "")]
public struct ValueString : IEquatable<ValueString>, IEquatable<string>, IComparable<ValueString>, IComparable<string>
{
    /// <summary>Represents the empty string. This field is read-only.</summary>
    public static ValueString Empty => string.Empty;

    private string? _value;

    /// <summary>
    /// returns System.String value
    /// </summary>
    /// <remarks>To support transparent XML serialization/deserialization</remarks>
    [XmlText, DataMember]
    public string Value
    {
        get => _value ?? string.Empty;
        set => _value = value;
    }

    public char this[int index] => Value[index];

    /// <summary>
    /// See <see cref="string.Length" />
    /// </summary>
    public int Length => _value is not null ? _value.Length : 0;

    /// <summary>Indicates whether the specified string an <see cref="F:System.String.Empty" /> string.</summary>
    public bool IsEmpty => string.IsNullOrEmpty(_value);

    /// <summary>Indicates whether the specified string is not empty string.</summary>
    public bool IsNotEmpty => !string.IsNullOrEmpty(_value);

    /// <summary>Indicates whether a specified string is empty, or consists only of white-space characters.</summary>
    public bool IsEmptyOrWhiteSpace => string.IsNullOrWhiteSpace(_value);

    /// <summary>Indicates whether a specified string is empty, or consists only of white-space characters.</summary>
    public bool IsNotEmptyOrWhiteSpace => !string.IsNullOrWhiteSpace(_value);

    /// <summary>Initializes a new instance of the <see cref="ValueString" /> class to the <see cref="F:System.String.Empty" /> value.</summary>
    public ValueString()
    {
        _value = string.Empty;
    }

    /// <summary>Initializes a new instance of the <see cref="ValueString" /> class to the given string value or <see cref="F:System.String.Empty" /> string if null.</summary>
    public ValueString(string? value)
    {
        _value = value ?? string.Empty;
    }

    /// <summary>Initializes a new instance of the <see cref="ValueString" /> class to the value indicated by a specified pointer to an array of Unicode characters.</summary>
    /// <param name="value">A pointer to a null-terminated array of Unicode characters.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">The current process does not have read access to all the addressed characters.</exception>
    /// <exception cref="T:System.ArgumentException"><paramref name="value">value</paramref> specifies an array that contains an invalid Unicode character, or <paramref name="value">value</paramref> specifies an address less than 64000.</exception>
    public ValueString(char[] value)
    {
        _value = new string(value);
    }

    /// <summary>Initializes a new instance of the <see cref="ValueString" /> class to the value indicated by a specified Unicode character repeated a specified number of times.</summary>
    /// <param name="c">A Unicode character.</param>
    /// <param name="count">The number of times c occurs.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="count">count</paramref> is less than zero.</exception>
    public ValueString(char c, int count)
    {
        _value = new string(c, count);
    }

    /// <summary>Initializes a new instance of the <see cref="ValueString" /> class to the value indicated by an array of Unicode characters, a starting character position within that array, and a length.</summary>
    /// <param name="value">An array of Unicode characters.</param>
    /// <param name="startIndex">The starting position within value.</param>
    /// <param name="length">The number of characters within value to use.</param>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="value">value</paramref> is null.</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="startIndex">startIndex</paramref> or <paramref name="length">length</paramref> is less than zero.   -or-   The sum of <paramref name="startIndex">startIndex</paramref> and <paramref name="length">length</paramref> is greater than the number of elements in <paramref name="value">value</paramref>.</exception>
    public ValueString(char[] value, int startIndex, int length)
    {
        _value = new string(value, startIndex, length);
    }


    /// <summary>
    /// implicit operator casts a string to SafeString
    /// </summary>
    public static implicit operator ValueString(string? value)
    {
        return new ValueString(value);
    }

    /// <summary>
    /// implicit operator casts a char[] to SafeString
    /// </summary>
    public static implicit operator ValueString(char[] value)
    {
        return new ValueString(value);
    }

    /// <summary>
    /// implicit operator casts a SafeString to string
    /// </summary>
    public static implicit operator string(ValueString val)
    {
        return val.Value;
    }

    /// <summary>
    /// Compare with <paramref name="strB"/>.
    /// </summary>
    /// <remarks>Delegates call to <see cref="string.Compare(string, string)" />.</remarks>
    [Pure]
    public int CompareTo(ValueString other)
    {
        return string.Compare(Value, other);
    }

    /// <summary>
    /// Compare with <paramref name="strB"/>.
    /// </summary>
    /// <remarks>Delegates call to <see cref="string.Compare(string, string)" />.</remarks>
    [Pure]
    public int CompareTo(string? strB)
    {
        return string.Compare(Value, strB);
    }

    /// <summary>
    /// Compare with <paramref name="strB"/>.
    /// </summary>
    /// <remarks>Delegates call to <see cref="string.Compare(string, string, StringComparison)" />.</remarks>
    [Pure]
    public int CompareTo(string? strB, StringComparison comparison)
    {
        return string.Compare(Value, strB, comparison);
    }

    /// <summary>
    /// Determine whether contains <paramref name="substring" />.
    /// </summary>
    /// <param name="substring">The string to seek.</param>
    [Pure]
    public bool Contains(string substring)
    {
        return Value.Contains(substring);
    }

    /// <summary>
    /// Determine whether contains <paramref name="substring" />.
    /// </summary>
    /// <param name="substring">The string to seek.</param>
    /// <param name="comparisonType">One of the enumeration values that specifies the rules for the search.</param>
    [Pure]
    public bool Contains(string substring, StringComparison comparisonType)
    {
        return Value.IndexOf(substring, comparisonType) >= 0;
    }

    /// <summary>
    /// See <see cref="string.CopyTo(int, char[], int, int)" />.
    /// </summary>
    [Pure]
    public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
    {
        Value.CopyTo(sourceIndex, destination, destinationIndex, count);
    }

    /// <summary>
    /// See <see cref="string.EndsWith(string, bool, CultureInfo)" />.
    /// </summary>
    [Pure]
    public bool EndsWith(string value, bool ignoreCase, CultureInfo culture)
    {
        return Value.EndsWith(value, ignoreCase, culture);
    }

    /// <summary>
    /// See <see cref="string.EndsWith(string, StringComparison)" />.
    /// </summary>
    [Pure]
    public bool EndsWith(string value)
    {
        return Value.EndsWith(value);
    }

    /// <summary>
    /// See <see cref="string.EndsWith(string, StringComparison)" />.
    /// </summary>
    [Pure]
    public bool EndsWith(string value, StringComparison comparisonType)
    {
        return Value.EndsWith(value, comparisonType);
    }

    /// <summary>
    /// See <see cref="string.Equals(object)" />.
    /// </summary>
    [Pure]
    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj is ValueString other)
        {
            return Value.Equals(other.Value);
        }

        return Value.Equals(obj);
    }

    /// <summary>Determines whether this instance and a specified object, which must also be a <see cref="ValueString" /> object, have the same value.</summary>
    /// <param name="other">The string to compare to this instance.</param>
    /// <returns>true if <paramref name="other"></paramref> is a <see cref="ValueString" /> and its value is the same as this instance; otherwise, false.</returns>
    [Pure]
    public bool Equals(ValueString other)
    {
        return Value.Equals(other.Value);
    }

    /// <summary>
    /// See <see cref="string.Equals(string)" />
    /// </summary>
    [Pure]
    public bool Equals(string? value)
    {
        return Value.Equals(value);
    }

    /// <summary>
    /// See <see cref="string.Equals(string, StringComparison)" />
    /// </summary>
    [Pure]
    public bool Equals(string? value, StringComparison comparisonType)
    {
        return Value.Equals(value, comparisonType);
    }

    /// <summary>
    /// See <see cref="string.GetHashCode()" />
    /// </summary>
    [Pure]
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    /// <summary>
    /// See <see cref="string.IndexOf(char)" />
    /// </summary>
    [Pure]
    public int IndexOf(char value)
    {
        return Value.IndexOf(value);
    }

    /// <summary>
    /// See <see cref="string.IndexOf(char, int)" />
    /// </summary>
    [Pure]
    public int IndexOf(char value, int startIndex)
    {
        return Value.IndexOf(value, startIndex);
    }

    /// <summary>
    /// See <see cref="string.IndexOf(string)" />
    /// </summary>
    [Pure]
    public int IndexOf(string value)
    {
        return Value.IndexOf(value);
    }

    /// <summary>
    /// See <see cref="string.IndexOf(string, StringComparison)" />
    /// </summary>
    [Pure]
    public int IndexOf(string value, StringComparison comparisonType)
    {
        return Value.IndexOf(value, comparisonType);
    }

    /// <summary>
    /// See <see cref="string.IndexOf(char, int, int)" />
    /// </summary>
    [Pure]
    public int IndexOf(char value, int startIndex, int count)
    {
        return Value.IndexOf(value, startIndex, count);
    }

    /// <summary>
    /// See <see cref="string.IndexOf(string, int)" />
    /// </summary>
    [Pure]
    public int IndexOf(string value, int startIndex)
    {
        return Value.IndexOf(value, startIndex);
    }

    /// <summary>
    /// See <see cref="string.IndexOf(string, int, StringComparison)" />
    /// </summary>
    [Pure]
    public int IndexOf(string value, int startIndex, StringComparison comparisonType)
    {
        return Value.IndexOf(value, startIndex, comparisonType);
    }

    /// <summary>
    /// See <see cref="string.IndexOf(string, int, int, StringComparison)" />
    /// </summary>
    [Pure]
    public int IndexOf(string value, int startIndex, int count)
    {
        return Value.IndexOf(value, startIndex, count);
    }

    /// <summary>
    /// See <see cref="string.IndexOf(string, int, int, StringComparison)" />
    /// </summary>
    [Pure]
    public int IndexOf(string value, int startIndex, int count, StringComparison comparisonType)
    {
        return Value.IndexOf(value, startIndex, count, comparisonType);
    }

    /// <summary>
    /// See <see cref="string.IndexOfAny(char[])" />
    /// </summary>
    [Pure]
    public int IndexOfAny(char[] anyOf)
    {
        return Value.IndexOfAny(anyOf);
    }

    /// <summary>
    /// See <see cref="string.IndexOfAny(char[], int)" />
    /// </summary>
    [Pure]
    public int IndexOfAny(char[] anyOf, int startIndex)
    {
        return Value.IndexOfAny(anyOf, startIndex);
    }

    /// <summary>
    /// See <see cref="string.IndexOfAny(char[], int, int)" />
    /// </summary>
    [Pure]
    public int IndexOfAny(char[] anyOf, int startIndex, int count)
    {
        return Value.IndexOfAny(anyOf, startIndex, count);
    }

    /// <summary>
    /// See <see cref="string.Insert(int, string)" />
    /// </summary>
    [Pure]
    public ValueString Insert(int startIndex, string value)
    {
        return Value.Insert(startIndex, value);
    }

    [Pure]
    public bool IsNormalized()
    {
        return Value.IsNormalized();
    }

    [Pure]
    public bool IsNormalized(NormalizationForm normalizationForm)
    {
        return Value.IsNormalized(normalizationForm);
    }

    /// <summary>
    /// See <see cref="string.LastIndexOf(char)" />
    /// </summary>
    [Pure]
    public int LastIndexOf(char value)
    {
        return Value.LastIndexOf(value);
    }

    /// <summary>
    /// See <see cref="string.LastIndexOf(char, int)" />
    /// </summary>
    [Pure]
    public int LastIndexOf(char value, int startIndex)
    {
        return Value.LastIndexOf(value, startIndex);
    }

    /// <summary>
    /// See <see cref="string.LastIndexOf(string, StringComparison)" />
    /// </summary>
    [Pure]
    public int LastIndexOf(string value)
    {
        return Value.LastIndexOf(value);
    }

    /// <summary>
    /// See <see cref="string.LastIndexOf(string, StringComparison)" />
    /// </summary>
    [Pure]
    public int LastIndexOf(string value, StringComparison comparisonType)
    {
        return Value.LastIndexOf(value, comparisonType);
    }

    /// <summary>
    /// See <see cref="string.LastIndexOf(char, int, int)" />
    /// </summary>
    [Pure]
    public int LastIndexOf(char value, int startIndex, int count)
    {
        return Value.LastIndexOf(value, startIndex, count);
    }

    /// <summary>
    /// See <see cref="string.LastIndexOf(string, int)" />
    /// </summary>
    [Pure]
    public int LastIndexOf(string value, int startIndex)
    {
        return Value.LastIndexOf(value, startIndex);
    }

    /// <summary>
    /// See <see cref="string.LastIndexOf(string, int, StringComparison)" />
    /// </summary>
    [Pure]
    public int LastIndexOf(string value, int startIndex, StringComparison comparisonType)
    {
        return Value.LastIndexOf(value, startIndex, comparisonType);
    }

    /// <summary>
    /// See <see cref="string.LastIndexOf(string, int, int, StringComparison)" />
    /// </summary>
    [Pure]
    public int LastIndexOf(string value, int startIndex, int count)
    {
        return Value.LastIndexOf(value, startIndex, count);
    }

    /// <summary>
    /// See <see cref="string.LastIndexOf(string, int, int, StringComparison)" />
    /// </summary>
    [Pure]
    public int LastIndexOf(string value, int startIndex, int count, StringComparison comparisonType)
    {
        return Value.LastIndexOf(value, startIndex, count, comparisonType);
    }

    /// <summary>
    /// See <see cref="string.LastIndexOfAny(char[])" />
    /// </summary>
    [Pure]
    public int LastIndexOfAny(char[] anyOf)
    {
        return Value.LastIndexOfAny(anyOf);
    }

    /// <summary>
    /// See <see cref="string.LastIndexOfAny(char[], int)" />
    /// </summary>
    [Pure]
    public int LastIndexOfAny(char[] anyOf, int startIndex)
    {
        return Value.LastIndexOfAny(anyOf, startIndex);
    }

    /// <summary>
    /// See <see cref="string.LastIndexOfAny(char[], int, int)" />
    /// </summary>
    [Pure]
    public int LastIndexOfAny(char[] anyOf, int startIndex, int count)
    {
        return Value.LastIndexOfAny(anyOf, startIndex, count);
    }

    /// <summary>
    /// See <see cref="string.Normalize()" />
    /// </summary>
    [Pure]
    public ValueString Normalize()
    {
        return Value.Normalize();
    }

    /// <summary>
    /// See <see cref="string.Normalize()" />
    /// </summary>
    [Pure]
    public ValueString Normalize(NormalizationForm normalizationForm)
    {
        return Value.Normalize(normalizationForm);
    }

    /// <summary>
    /// See <see cref="string.PadLeft(int)" />
    /// </summary>
    [Pure]
    public ValueString PadLeft(int totalWidth)
    {
        return Value.PadLeft(totalWidth);
    }

    /// <summary>
    /// See <see cref="string.PadLeft(int, char)" />
    /// </summary>
    [Pure]
    public ValueString PadLeft(int totalWidth, char paddingChar)
    {
        return Value.PadLeft(totalWidth, paddingChar);
    }

    /// <summary>
    /// See <see cref="string.PadRight(int)" />
    /// </summary>
    [Pure]
    public ValueString PadRight(int totalWidth)
    {
        return Value.PadRight(totalWidth);
    }

    /// <summary>
    /// See <see cref="string.PadRight(int, char)" />
    /// </summary>
    [Pure]
    public ValueString PadRight(int totalWidth, char paddingChar)
    {
        return Value.PadRight(totalWidth, paddingChar);
    }

    /// <summary>
    /// See <see cref="string.Remove(int)" />
    /// </summary>
    [Pure]
    public ValueString Remove(int startIndex)
    {
        return Value.Remove(startIndex);
    }

    /// <summary>
    /// See <see cref="string.Remove(int, int)" />
    /// </summary>
    [Pure]
    public ValueString Remove(int startIndex, int count)
    {
        return Value.Remove(startIndex, count);
    }

    /// <summary>
    /// See <see cref="string.Replace(char, char)" />
    /// </summary>
    [Pure]
    public ValueString Replace(char oldChar, char newChar)
    {
        return Value.Replace(oldChar, newChar);
    }

    /// <summary>
    /// See <see cref="string.Replace(string, string)" />
    /// </summary>
    [Pure]
    public ValueString Replace(string oldValue, string newValue)
    {
        return Value.Replace(oldValue, newValue);
    }

    /// <summary>
    /// See <see cref="string.Split(char[])" />
    /// </summary>
    [Pure]
    public string[] Split(params char[] separator)
    {
        return Value.Split(separator);
    }

    /// <summary>Splits a string into substrings based on the character. You can specify whether the substrings include empty array elements.</summary>
    /// <param name="separator">A character that delimits the substrings in this string, an empty array that contains no delimiters, or null.</param>
    /// <param name="options"><see cref="F:System.StringSplitOptions.RemoveEmptyEntries" /> to omit empty array elements from the array returned; or <see cref="F:System.StringSplitOptions.None" /> to include empty array elements in the array returned.</param>
    /// <returns>An array whose elements contain the substrings in this string that are delimited by one or more characters in <paramref name="separator">separator</paramref>. For more information, see the Remarks section.</returns>
    /// <exception cref="T:System.ArgumentException"><paramref name="options">options</paramref> is not one of the <see cref="ValueString" /> values.</exception>
    [Pure]
    public string[] Split(char separator, StringSplitOptions options = StringSplitOptions.None)
    {
        if (string.IsNullOrEmpty(_value))
        {
            return [];
        }
#if NETCOREAPP
        return _value.Split(separator, options);
#else
        return _value!.Split([separator], options);
#endif
    }

    /// <summary>Splits a string into substrings based on the character. You can specify whether the substrings include empty array elements.</summary>
    /// <param name="separator">A character that delimits the substrings in this string, an empty array that contains no delimiters, or null.</param>
    /// <param name="options"><see cref="F:System.StringSplitOptions.RemoveEmptyEntries" /> to omit empty array elements from the array returned; or <see cref="F:System.StringSplitOptions.None" /> to include empty array elements in the array returned.</param>
    /// <returns>An array whose elements contain the substrings in this string that are delimited by one or more characters in <paramref name="separator">separator</paramref>. For more information, see the Remarks section.</returns>
    /// <exception cref="T:System.ArgumentException"><paramref name="options">options</paramref> is not one of the <see cref="ValueString" /> values.</exception>
    [Pure]
    public string[] Split(string separator, StringSplitOptions options = StringSplitOptions.None)
    {
        if (string.IsNullOrEmpty(_value))
        {
            return [];
        }
#if NETCOREAPP
        return _value.Split(separator, options);
#else
        return _value!.Split([separator], options);
#endif
    }

    /// <summary>
    /// <see cref="string.Split(char[], StringSplitOptions)" />
    /// </summary>
    [Pure]
    public string[] Split(char[] separator, StringSplitOptions options)
    {
        return Value.Split(separator, options);
    }

    /// <summary>
    /// <see cref="string.Split(string[], StringSplitOptions)" />
    /// </summary>
    [Pure]
    public string[] Split(string[] separator, StringSplitOptions options = StringSplitOptions.None)
    {
        return Value.Split(separator, options);
    }

    /// <summary>
    /// <see cref="string.Split(string[], int, StringSplitOptions)" />
    /// </summary>
    [Pure]
    public string[] Split(string[] separator, int count, StringSplitOptions options = StringSplitOptions.None)
    {
        return Value.Split(separator, count, options);
    }

    /// <summary>
    /// <see cref="string.StartsWith(string)" />
    /// </summary>
    [Pure]
    public bool StartsWith(string value)
    {
        return Value.StartsWith(value);
    }

    /// <summary>
    /// <see cref="string.StartsWith(string, StringComparison)" />
    /// </summary>
    [Pure]
    public bool StartsWith(string value, StringComparison comparisonType)
    {
        return Value.StartsWith(value, comparisonType);
    }

    [Pure]
    public bool StartsWith(string value, bool ignoreCase, CultureInfo culture)
    {
        return Value.StartsWith(value, ignoreCase, culture);
    }

    /// <summary>
    /// <see cref="string.Substring(int)" />
    /// </summary>
    [Pure]
    public ValueString Substring(int startIndex)
    {
        return Value.Substring(startIndex);
    }

    /// <summary>
    /// <see cref="string.Substring(int, int)" />
    /// </summary>
    [Pure]
    public ValueString Substring(int startIndex, int length)
    {
        return Value.Substring(startIndex, length);
    }

    /// <summary>
    /// <see cref="string.ToCharArray()" />
    /// </summary>
    [Pure]
    public char[] ToCharArray()
    {
        return Value.ToCharArray();
    }

    /// <summary>
    /// <see cref="string.ToLower()" />
    /// </summary>
    [Pure]
    public ValueString ToLower()
    {
        return Value.ToLower();
    }

    /// <summary>
    /// <see cref="string.ToLowerInvariant()" />
    /// </summary>
    [Pure]
    public ValueString ToLowerInvariant()
    {
        return Value.ToLowerInvariant();
    }

    /// <summary>
    /// <see cref="string.ToString()" />
    /// </summary>
    [Pure]
    public override string ToString()
    {
        return Value;
    }

    /// <summary>
    /// <see cref="string.ToUpper()" />
    /// </summary>
    [Pure]
    public ValueString ToUpper()
    {
        return Value.ToUpper();
    }

    /// <summary>
    /// <see cref="string.ToUpperInvariant()" />
    /// </summary>
    [Pure]
    public ValueString ToUpperInvariant()
    {
        return Value.ToUpperInvariant();
    }

    /// <summary>
    /// <see cref="string.Trim()" />
    /// </summary>
    [Pure]
    public ValueString Trim()
    {
        return Value.Trim();
    }

    /// <summary>
    /// <see cref="string.Trim(char[])" />
    /// </summary>
    [Pure]
    public ValueString Trim(params char[] trimChars)
    {
        return Value.Trim(trimChars);
    }

    /// <summary>
    /// <see cref="string.TrimEnd(char[])" />
    /// </summary>
    [Pure]
    public ValueString TrimEnd(params char[] trimChars)
    {
        return Value.TrimEnd(trimChars);
    }

    /// <summary>
    /// <see cref="string.TrimStart(char[])" />
    /// </summary>
    [Pure]
    public ValueString TrimStart(params char[] trimChars)
    {
        return Value.TrimStart(trimChars);
    }

    /// <summary>
    /// Retrieves a substring from this instance. The substring starts at 0 and has a specified length or less, if the length less then charCount.
    /// </summary>
    [Pure]
    public ValueString Left(int charCount)
    {
        string value = Value;
        return charCount >= value.Length
            ? value
            : value.Substring(0, charCount);
    }

    /// <summary>
    /// Retrieves a substring from this instance. The substring starts at Value.Length - charCount position and has a specified length or less, if the length less then charCount.
    /// </summary>
    [Pure]
    public ValueString Right(int charCount)
    {
        string value = Value;
        return charCount >= value.Length
            ? value
            : value.Substring(value.Length - charCount, charCount);
    }

    ////public IEnumerator<char> GetEnumerator()
    ////{
    ////    return Value.GetEnumerator();
    ////}

    ////IEnumerator IEnumerable.GetEnumerator()
    ////{
    ////    return Value.GetEnumerator();
    ////}

    /// <summary>Determines whether two specified strings have the same value.</summary>
    /// <param name="left">The first string to compare.</param>
    /// <param name="right">The second string to compare.</param>
    /// <returns>true if the value of <paramref name="left" /> is the same as the value of <paramref name="right">b</paramref>; otherwise, false.</returns>
    public static bool operator ==(ValueString left, string? right)
    {
        return left.Equals(right);
    }

    /// <summary>Determines whether two specified strings have different values.</summary>
    /// <param name="left">The first string to compare.</param>
    /// <param name="right">The second string to compare.</param>
    /// <returns>true if the value of <paramref name="left" /> is different from the value of <paramref name="right" />; otherwise, false.</returns>
    public static bool operator !=(ValueString left, string? right)
    {
        return !(left == right);
    }

    ///// <summary>Determines whether two specified strings have the same value.</summary>
    ///// <param name="left">The first string to compare.</param>
    ///// <param name="right">The second string to compare.</param>
    ///// <returns>true if the value of <paramref name="left" /> is the same as the value of <paramref name="right" />; otherwise, false.</returns>
    ////public static bool operator ==(ValueString left, ValueString right) => left.Equals(right);

    ///// <summary>Determines whether two specified strings have different values.</summary>
    ///// <param name="left">The first string to compare.</param>
    ///// <param name="right">The second string to compare.</param>
    ///// <returns>true if the value of <paramref name="left" /> is different from the value of <paramref name="right" />; otherwise, false.</returns>
    ////public static bool operator !=(ValueString left, ValueString right) => !(left == right);

    ///// <summary>Determines if left.CompareTo(right) less than 0</summary>
    ////public static bool operator <(ValueString left, ValueString right)
    ////{
    ////    return left.CompareTo(right) < 0;
    ////}

    ///// <summary>Determines if left.CompareTo(right) less than or equals 0</summary>
    ////public static bool operator <=(ValueString left, ValueString right)
    ////{
    ////    return left.CompareTo(right) <= 0;
    ////}

    ///// <summary>Determines if left.CompareTo(right) greater than 0 </summary>
    ////public static bool operator >(ValueString left, ValueString right)
    ////{
    ////    return left.CompareTo(right) > 0;
    ////}

    ///// <summary>Determines if left.CompareTo(right) greater than or equals 0</summary>
    ////public static bool operator >=(ValueString left, ValueString right)
    ////{
    ////    return left.CompareTo(right) >= 0;
    ////}

    ///// <summary>Determines if left.CompareTo(right) less than 0</summary>
    ////public static bool operator <(ValueString left, string? right)
    ////{
    ////    return left.CompareTo(right) < 0;
    ////}

    ///// <summary>Determines if left.CompareTo(right) less than or equals 0</summary>
    ////public static bool operator <=(ValueString left, string? right)
    ////{
    ////    return left.CompareTo(right) <= 0;
    ////}

    ///// <summary>Determines if left.CompareTo(right) greater than 0 </summary>
    ////public static bool operator >(ValueString left, string? right)
    ////{
    ////    return left.CompareTo(right) > 0;
    ////}

    ///// <summary>Determines if left.CompareTo(right) greater than or equals 0</summary>
    ////public static bool operator >=(ValueString left, string? right)
    ////{
    ////    return left.CompareTo(right) >= 0;
    ////}
}