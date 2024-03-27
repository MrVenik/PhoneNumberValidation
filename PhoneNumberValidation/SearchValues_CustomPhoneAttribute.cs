using System.Buffers;
using System.ComponentModel.DataAnnotations;

namespace PhoneNumberValidation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class SearchValues_CustomPhoneAttribute : DataTypeAttribute
{
    private static SearchValues<char> DigitSearchValues { get; } = SearchValues.Create("0123456789");

    public SearchValues_CustomPhoneAttribute() : base(DataType.PhoneNumber)
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

        if (possibleDigits.IndexOfAnyExcept(DigitSearchValues) != -1)
        {
            return false;
        }

        // base.IsValid - always returns true
        return base.IsValid(value);
    }
}
