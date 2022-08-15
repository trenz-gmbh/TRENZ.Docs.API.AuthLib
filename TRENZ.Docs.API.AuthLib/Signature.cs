using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TRENZ.Docs.API.AuthLib
{
    public class Signature
    {
        public static string SignEndpoint(string endpoint, IDictionary<string, string?> queryParams, string secret, string? signatureKey = null)
        {
            var uri = new UriBuilder(endpoint);

            var query = HttpUtility.ParseQueryString(uri.Query);
            foreach (var (key, value) in queryParams)
            {
                query[key] = value;
            }

            var payload = query.ToString()!;
            var signature = GenerateSignature(secret, payload);
            query[signatureKey ?? "signature"] = signature;

            uri.Query = query.ToString();

            return uri.ToString();
        }

        public static bool ValidateSignature(string query, string secret, string? signatureKey = null)
        {
            signatureKey ??= "signature";

            var collection = HttpUtility.ParseQueryString(query);
            if (collection[signatureKey] == null)
                return false;

            var signature = collection[signatureKey];
            collection.Remove(signatureKey);

            var payload = collection.ToString()!;
            var expectedSignature = GenerateSignature(secret, payload);

            return signature == expectedSignature;
        }

        public static string GenerateSignature(string secret, string payload)
        {
            var key = Encoding.UTF8.GetBytes(secret);
            using var hash = new HMACSHA256(key);
            var signature = hash.ComputeHash(Encoding.UTF8.GetBytes(payload));
            return Convert.ToBase64String(signature);
        }
    }
}