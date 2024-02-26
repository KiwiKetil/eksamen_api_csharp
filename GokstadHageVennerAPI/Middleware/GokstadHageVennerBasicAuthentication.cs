using GokstadHageVennerAPI.Services.Interfaces;
using System.Diagnostics;

namespace GokstadHageVennerAPI.Middleware;

public class GokstadHageVennerBasicAuthentication : IMiddleware
{
    private readonly IMemberService _memberService;
    private readonly ILogger _logger;
    public GokstadHageVennerBasicAuthentication(IMemberService memberService, ILogger<GokstadHageVennerBasicAuthentication> logger)
    {
        _memberService = memberService;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.Request.Path != null && context.Request.Path.Value != null && context.Request.Path.Value.EndsWith("/members/register", StringComparison.OrdinalIgnoreCase) == true && 
            context.Request.Method == "POST")
        {
            await next(context);
            return;
        }

        try
        {
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                _logger.LogDebug("Missing authorization header");
                throw new UnauthorizedAccessException("Not Authorized - 'Authorization' missing in HTTP-header");
            }

            var authHeader = context.Request.Headers.Authorization;

            string base64string = authHeader.ToString().Split(" ")[1];

            string user_password = DecodeBase64String(base64string);

            var arr = user_password.Split(":");
            if (arr.Length != 2)
            {
                _logger.LogDebug("UserName and Password Error");
                throw new FormatException("UserName and Password Error");
            }
            string userName = arr[0];
            string password = arr[1];

            int userId = await _memberService.GetAuthenticatedIdAsync(userName, password);
            if (userId == 0)
            {
                _logger.LogDebug("Unauthorized Access Exception - User not authenticated");
                throw new UnauthorizedAccessException("No access to this API");
            }

            if (!context.Items.ContainsKey("UserId") && !context.Items.ContainsKey("UserName"))
            
            context.Items["UserId"] = userId;
            context.Items["UserName"] = userName;          

            var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            _logger.LogInformation("User {UserName} successfully logged in at {Timestamp} from IP {IPAddress}", userName, DateTime.UtcNow, ipAddress);

            await next(context);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogError("Unauthorized Access Exception");
            await Results.Problem(
             title: "Unauthorized: No access to this API",
             statusCode: StatusCodes.Status401Unauthorized,
             detail: ex.Message,
             extensions: new Dictionary<string, Object?>
             {
                    { "traceId", Activity.Current?.Id  }
             })
             .ExecuteAsync(context);
        }
    }

    private string DecodeBase64String(string base64string)
    {
        byte[] base64bytes = System.Convert.FromBase64String(base64string);
        string username_and_password = System.Text.Encoding.UTF8.GetString(base64bytes);
        return username_and_password;
    }
}
