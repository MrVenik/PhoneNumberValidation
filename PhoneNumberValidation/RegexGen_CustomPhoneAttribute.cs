using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PhoneNumberValidation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public sealed partial class RegexGen_CustomPhoneAttribute : DataTypeAttribute
{
    public RegexGen_CustomPhoneAttribute() : base(DataType.PhoneNumber)
    {
    }

    public override bool IsValid(object? value)
    {
        // If value is required then consumer should use RequiredAttribute with this attribute
        if (value is null)
        {
            return true;
        }

        if (value is not string phoneNumber)
        {
            return false;
        }

        if (!PhoneNumberRegex().IsMatch(phoneNumber))
        {
            return false;
        }

        // base.IsValid - always returns true
        return base.IsValid(value);
    }

    [GeneratedRegex(Constants.RegExpPattern)]
    private static partial Regex PhoneNumberRegex();
}
