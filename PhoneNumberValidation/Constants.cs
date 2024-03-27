using System.Diagnostics.CodeAnalysis;

namespace PhoneNumberValidation;

public static class Constants
{
    /// <summary>
    /// Shortest possible phone number length without '+' prefix.
    /// </summary>
    public const int MinPhoneNumberLength = 7;

    /// <summary>
    /// Longest possible phone number length without '+' prefix.
    /// </summary>
    public const int MaxPhoneNumberLength = 15;

    /// <summary>
    /// Regular expression for phone number validation
    /// </summary>
    [StringSyntax(StringSyntaxAttribute.Regex)]
    public const string RegExpPattern = @"^\+(?:[0-9]){6,14}[0-9]$";
}
