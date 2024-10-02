using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public TokenValidationMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Trim();

        if (!string.IsNullOrEmpty(token))
        {
            if (TokenBlacklist.IsTokenBlacklisted(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Issuer"],
                ClockSkew = TimeSpan.Zero // No tolerance for token expiration time
            };

            try
            {
                // Validate the token and extract the ClaimsPrincipal
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                // Extract claims
                var branchIdClaim = principal.Claims.FirstOrDefault(c => c.Type == "branchId")?.Value;
                var companyIdClaim = principal.Claims.FirstOrDefault(c => c.Type == "companyId")?.Value;

                // Store claims in HttpContext for later use
                context.Items["BranchId"] = branchIdClaim;
                context.Items["CompanyId"] = companyIdClaim;

                // Optionally, you can attach the ClaimsPrincipal to HttpContext.User
                context.User = principal;
            }
            catch (SecurityTokenExpiredException)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token expired");
                return;
            }
            catch (Exception)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid token");
                return;
            }
        }

        await _next(context);
    }
}
