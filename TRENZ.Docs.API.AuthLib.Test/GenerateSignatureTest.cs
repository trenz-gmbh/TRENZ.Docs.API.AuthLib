using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TRENZ.Docs.API.AuthLib.Test;

[TestClass]
public class GenerateSignatureTest
{
    public static IEnumerable<object[]> GenerateSignatureValuesProvider()
    {
        yield return new object[] { "secret", "payload", "uC/LeRrOxXhZuYm0MKgmSIzi5Hn9+SMmvQoug3WkK6Q=" };
        yield return new object[] { "another", "payload", "Nu7Dn8Qb6R2z+twrtqr4Hx2HyuOn9Ir2UooA8hNw+qU=" };
        yield return new object[] { "another", "one", "k9Y8AElj6Rb7qdBZzq8Nq/0Pr2qOrcdZsnop86O06mI=" };
    }

    [DataTestMethod]
    [DynamicData(nameof(GenerateSignatureValuesProvider), DynamicDataSourceType.Method)]
    public void TestGenerateSignature(string secret, string payload, string expected)
    {
        Assert.AreEqual(Signature.GenerateSignature(secret, payload), expected);
    }
}