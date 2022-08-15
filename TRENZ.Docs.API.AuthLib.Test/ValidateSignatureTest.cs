using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TRENZ.Docs.API.AuthLib.Test;

[TestClass]
public class ValidateSignatureTest
{
    public static IEnumerable<object[]> ValidateSignatureValuesProvider()
    {
        yield return new object[] { "missing=key", "secret", "signature", false };
        yield return new object[] { "invalid=signature&signature=", "secret", "signature", false };
        yield return new object[] { "actually=ok&signature=0aA2WwgAlcyRnX9oek7m9NmOjt1W4e9UVs5qfj8CYNQ=", "secret", "signature", true };
    }

    [DataTestMethod]
    [DynamicData(nameof(ValidateSignatureValuesProvider), DynamicDataSourceType.Method)]
    public void TestValidateSignature(string query, string secret, string? signatureKey, bool expected)
    {
        Assert.AreEqual(Signature.ValidateSignature(query, secret, signatureKey), expected);
    }
}