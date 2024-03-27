using System.ComponentModel.DataAnnotations;

namespace PhoneNumberValidation.Tests;

public sealed class SearchValues_CustomPhoneAttributeTests : PhoneNumberValidationTests<SearchValues_CustomPhoneAttribute>;

public sealed class TryParseIntoUlong_CustomPhoneAttributeTests : PhoneNumberValidationTests<TryParseIntoUlong_CustomPhoneAttribute>;

public sealed class NaiveSearch_CustomPhoneAttributeTests : PhoneNumberValidationTests<NaiveSearch_CustomPhoneAttribute>;

public sealed class RegexGen_CustomPhoneAttributeTests : PhoneNumberValidationTests<RegexGen_CustomPhoneAttribute>;

public abstract class PhoneNumberValidationTests<TValidator>
    where TValidator : ValidationAttribute, new()
{
    public static IEnumerable<object[]> ValidPhoneNumbers =>
    [
        ["+1111111"],
        ["+380999999999"],
        ["+111111111111111"],
    ];

    public static IEnumerable<object[]> InvalidPhoneNumbers =>
    [
        ["+11111"],
        ["1111111"],
        ["+1111111111111111"],
    ];

    [Theory]
    [MemberData(nameof(InvalidPhoneNumbers))]
    public void IsValid_WhenPhoneNumberIsInvalid_ShouldBeFalse(string phoneNumber)
    {
        // Arrange
        var validator = new TValidator();

        // Act
        var isValid = validator.IsValid(phoneNumber);

        // Assert
        Assert.False(isValid);
    }

    [Theory]
    [MemberData(nameof(ValidPhoneNumbers))]
    public void IsValid_WhenPhoneNumberIsValid_ShouldBeTrue(string phoneNumber)
    {
        // Arrange
        var validator = new TValidator();

        // Act
        var isValid = validator.IsValid(phoneNumber);

        // Assert
        Assert.True(isValid);
    }
}
