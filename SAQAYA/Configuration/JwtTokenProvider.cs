using SAQAYA.BusinessLogic.IServices;
using SAQAYA.BusinessLogic.Models.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Foundation.AuthApi.Providers
{
    public class JwtTokenProvider : IJwtTokenProvider
    {


        private readonly AuthenticationConfiguration _AuthenticationSettingsModel;

        public JwtTokenProvider(IOptions<AuthenticationConfiguration> authenticationSettingsModel)
        {
            _AuthenticationSettingsModel = authenticationSettingsModel.Value;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_AuthenticationSettingsModel.JwtKey)),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid_access_token");

            return principal;
        }


        public (string token, DateTime validTo) GenerateJwtTokenAsync(string UserId, string Username)
        {

            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier,UserId)
            };
            //claims.AddRange(userClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_AuthenticationSettingsModel.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddHours(Convert.ToDouble(_AuthenticationSettingsModel.TokenExpireDurationInHours));

            var token = new JwtSecurityToken(_AuthenticationSettingsModel.Issuer, _AuthenticationSettingsModel.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );
            var result = new JwtSecurityTokenHandler().WriteToken(token);
            return (result, token.ValidTo);
        }


    }
}
