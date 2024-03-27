using System.ComponentModel.DataAnnotations;
using BenchmarkDotNet.Attributes;

namespace PhoneNumberValidation;

[MemoryDiagnoser(true)]
public class Benches
{
    [Params("+1111111", "+380999999999", "+111111111111111")]
    public string PhoneNumber { get; set; }

    private readonly SearchValues_CustomPhoneAttribute _searchValues_CustomPhoneAttribute = new();
    private readonly TryParseIntoUlong_CustomPhoneAttribute _tryParseIntoUlong_CustomPhoneAttribute = new();
    private readonly NaiveSearch_CustomPhoneAttribute _naiveSearch_CustomPhoneAttribute = new();
    private readonly RegexGen_CustomPhoneAttribute _regexGen_CustomPhoneAttribute = new();
    private readonly PhoneAttribute _phoneAttribute = new();
    private readonly RegularExpressionAttribute _regularExpressionAttribute = new(Constants.RegExpPattern);

    [Benchmark]
    public void SearchValues_CustomPhoneAttribute() => _searchValues_CustomPhoneAttribute.IsValid(PhoneNumber);

    [Benchmark]
    public void TryParseIntoUlong_CustomPhoneAttribute() => _tryParseIntoUlong_CustomPhoneAttribute.IsValid(PhoneNumber);

    [Benchmark]
    public void NaiveSearch_CustomPhoneAttribute() => _naiveSearch_CustomPhoneAttribute.IsValid(PhoneNumber);

    [Benchmark]
    public void RegexGen_CustomPhoneAttribute() => _regexGen_CustomPhoneAttribute.IsValid(PhoneNumber);

    [Benchmark]
    public void PhoneAttribute() => _phoneAttribute.IsValid(PhoneNumber);

    [Benchmark]
    public void RegularExpressionAttribute() => _regularExpressionAttribute.IsValid(PhoneNumber);
}
