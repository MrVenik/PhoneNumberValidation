using System.ComponentModel.DataAnnotations;

namespace PhoneNumberValidation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class TryParseIntoUlong_CustomPhoneAttribute : DataTypeAttribute
{
    public TryParseIntoUlong_CustomPhoneAttribute() : base(DataType.PhoneNumber)
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

        var phoneNumberSpan = phoneNumber.AsSpan();

        if (phoneNumberSpan is not ['+', .. var possibleDigits])
        {
            return false;
        }

        if (possibleDigits is { Length: < Constants.MinPhoneNumberLength or > Constants.MaxPhoneNumberLength })
        {
            return false;
        }

        // Here used ulong.TryParse, because number with 15 digits can be bigger than int.MaxValue, and negative numbers is not supported
        if (!ulong.TryParse(possibleDigits, out _))
        {
            return false;
        }

        // base.IsValid - always returns true
        return base.IsValid(value);
    }
}
