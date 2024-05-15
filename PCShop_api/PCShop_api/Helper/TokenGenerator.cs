using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PCShop_api.Helper
{
    public class TokenGenerator
    {
        public static string Generate()
        {
            var key = Guid.NewGuid().ToByteArray();
            var securityKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "subject"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken
                (
                    issuer: "bios",
                    audience: "bios",
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(90),
                    signingCredentials: credentials
                );
            var jwtHandler = new JwtSecurityTokenHandler();
            return jwtHandler.WriteToken(token);
        }

        public static string GenerisiIme(int size)
        {
            // Characters except I, l, O, 1, and 0 to decrease confusion when hand typing tokens
            var charSet = "ABCDEFGHJKLMNPQRSTUVWXYZ".ToLower();
            var chars = charSet.ToCharArray();
            var data = new byte[1];
#pragma warning disable SYSLIB0023 // Type or member is obsolete
            var crypto = new RNGCryptoServiceProvider();
#pragma warning restore SYSLIB0023 // Type or member is obsolete
            crypto.GetNonZeroBytes(data);
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            var result = new StringBuilder(size);
            foreach (var b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            var s = result.ToString();
            return "S" + result;
        }
    }
}
