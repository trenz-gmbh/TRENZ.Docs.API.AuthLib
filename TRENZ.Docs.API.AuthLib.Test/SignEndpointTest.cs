using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TRENZ.Docs.API.AuthLib.Test;

[TestClass]
public class SignEndpointTest
{
    public static IEnumerable<object[]> SignEndpointValuesProvider()
    {
        yield return new object[]
        {
            "http://localhost/test",
            new Dictionary<string, string?> { { "val", "key" } },
            "secret",
            "signature",
            "http://localhost:80/test?val=key&signature=M5dHxXpmj4cUbO72Q4TsFWiQbImBkRMa2dnH48RKnjM%3d",
        };

        yield return new object[]
        {
            "http://localhost/test?existing=param",
            new Dictionary<string, string?> { { "val", "key" } },
            "secret",
            "signature",
            "http://localhost:80/test?existing=param&val=key&signature=VvKhsJMRbJsWAi318BYkOa0%2bEHMb9Lh8CYEdTkWv4j4%3d",
        };
    }

    [DataTestMethod]
    [DynamicData(nameof(SignEndpointValuesProvider), DynamicDataSourceType.Method)]
    public void TestSignEndpoint(string endpoint, IDictionary<string, string?> query, string secret, string? signatureKey, string expected)
    {
        Assert.AreEqual(Signature.SignEndpoint(endpoint, query, secret, signatureKey), expected);
    }
}