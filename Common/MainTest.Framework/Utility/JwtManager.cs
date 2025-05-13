using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace MainTest.Framework.Utility
{
    public class JwtManager
    {
        public static TokenDetail GenerateToken(TokenRequest tokenRequest, int expiresInMinutes, string secretKey)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = System.Text.Encoding.ASCII.GetBytes(secretKey);
                var securityAlgorithm = SecurityAlgorithms.HmacSha256Signature;

                var claims = tokenRequest.Claims.Select(p=> new Claim(p.Key,p.Value)).ToArray();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(expiresInMinutes),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), securityAlgorithm)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return new TokenDetail
                {
                    Token = tokenHandler.WriteToken(token),
                    RefreshToken = Guid.NewGuid().ToString(),
                    ExpireDateTime = tokenDescriptor.Expires.Value,
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<System.Security.Claims.Claim> VerifyToekn(string token, string secrestKey)
        {
            try
            {
                if (token.StartsWith("Bearer ")) token = token.Replace("Bearer ", "");
                var key = System.Text.Encoding.ASCII.GetBytes(secrestKey);

                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = System.TimeSpan.Zero
                }, out Microsoft.IdentityModel.Tokens.SecurityToken validatedToken);

                var jwtToken = validatedToken as System.IdentityModel.Tokens.Jwt.JwtSecurityToken;

                if (jwtToken == null)
                {
                    return null;
                }

                return jwtToken.Claims.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public class TokenRequest
        {
            public string UserId { get; set; }
            public string UserName { get; set; }
            public Dictionary<string,string> Claims { get; set; }
        }

        public class TokenDetail
        {
            public string Token { get; set; }
            public string RefreshToken { get; set; }
            public DateTime ExpireDateTime { get; set; }
        }
    }
}
