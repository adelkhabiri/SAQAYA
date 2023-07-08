using System.Security.Claims;

namespace SAQAYA.BusinessLogic.IServices
{
    public interface IJwtTokenProvider
    {
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        (string token, DateTime validTo) GenerateJwtTokenAsync(string UserId, string Username/*, IEnumerable<Claim> userClaims*/);

    }
}
