using System.IdentityModel.Tokens.Jwt;

public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;

    public TokenValidationMiddleware(RequestDelegate next)
    {
        _next = next;
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

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                // Extract claims
                var branchIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "branchId")?.Value;
                var companyIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "companyId")?.Value;

                // You can do something with branchId and companyId here
                // For example, store them in HttpContext.Items for later use
                context.Items["BranchId"] = branchIdClaim;
                context.Items["CompanyId"] = companyIdClaim;
            }
            catch (Exception)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
        }

        await _next(context);
    }

}
