using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;

namespace Ykotika.Security
{
    public class JwtProvider(IOptions<AccessTokenOptions> options) : IJwtProvider
    {
        private readonly AccessTokenOptions _options = options.Value;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();
        private const string AES_IV = "sdofisldjfklsd3ldedddsddhiew";

        public string GenerateAccessToken(User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email.ToString()),
            };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            var signingCredentials = new SigningCredentials(

                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.JwtOptions.SecretKey)),

                    SecurityAlgorithms.HmacSha256
                );

            var token = new JwtSecurityToken(

                    claims: claims,

                    signingCredentials: signingCredentials,

                    expires: DateTime.UtcNow.AddHours(_options.JwtOptions.ExpiresHours)
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
        public string GenerateEmailVerificationToken(Guid userId, string userEmail)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, userId.ToString()),
                new(ClaimTypes.Email, userEmail)

            };
            var signingCredentials = new SigningCredentials(

                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.JwtOptions.SecretKey)),

                SecurityAlgorithms.HmacSha256
            );
            var token = new JwtSecurityToken(

                 claims: claims,

                 signingCredentials: signingCredentials,

                 expires: DateTime.UtcNow.AddMinutes(30)
             );
            var tokenString = _jwtSecurityTokenHandler.WriteToken(token);

            return EncryptToken(tokenString);
        }
        public bool VerifyEmailToken(string token, Guid userId, string userEmail)
        {
            string decryptedToken;
            try
            {
                decryptedToken = DecryptToken(token);
            }
            catch
            {
                return false;
            }

            if (!_jwtSecurityTokenHandler.CanReadToken(decryptedToken))
            {
                return false;
            }

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.JwtOptions.SecretKey))
            };

            try
            {
                var principal = _jwtSecurityTokenHandler.ValidateToken(decryptedToken, tokenValidationParameters, out var validatedToken);

                if (validatedToken is JwtSecurityToken jwtToken &&
                    jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    var tokenUserId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    var tokenEmail = principal.FindFirst(ClaimTypes.Email)?.Value;

                    if (tokenUserId == userId.ToString() && tokenEmail == userEmail)
                    {
                        return true;
                    }
                }
            }
            catch (Exception) { }

            return false;
        }
        private string EncryptToken(string token)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(_options.JwtOptions.SecretKey.PadRight(32).Substring(0, 32));
            aes.IV = Encoding.UTF8.GetBytes(AES_IV.PadRight(16).Substring(0, 16));

            using var encryptor = aes.CreateEncryptor();
            var tokenBytes = Encoding.UTF8.GetBytes(token);
            var encryptedBytes = encryptor.TransformFinalBlock(tokenBytes, 0, tokenBytes.Length);

            return Convert.ToBase64String(encryptedBytes);
        }
        private string DecryptToken(string encryptedToken)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(_options.JwtOptions.SecretKey.PadRight(32).Substring(0, 32));
            aes.IV = Encoding.UTF8.GetBytes(AES_IV.PadRight(16).Substring(0, 16));

            using var decryptor = aes.CreateDecryptor();
            var encryptedBytes = Convert.FromBase64String(encryptedToken);
            var decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

            return Encoding.UTF8.GetString(decryptedBytes);
        }
        public string GenerateRefreshToken()
        {
            var signingCredentials = new SigningCredentials(

                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.JwtOptions.SecretKey)),

                    SecurityAlgorithms.HmacSha256
                );

            var token = new JwtSecurityToken(

                    signingCredentials: signingCredentials,

                    expires: DateTime.UtcNow.AddHours(_options.JwtOptions.ExpiresHours)
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.JwtOptions.SecretKey)),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
    }
}
