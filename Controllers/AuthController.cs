// using Microsoft.AspNetCore.Authentication;
// using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Configuration;
using Google.Apis.Auth; // Ensure you have this package installed
// using System.Collections.Generic;
// using System.Threading.Tasks;
namespace cakeweb.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("googlelogin")]
    public async Task<IActionResult> GoogleLogin()
    {
        // Get credentials and CSRF token from the request
        string? formCredential = Request.Form["credential"];
        string? formToken = Request.Form["g_csrf_token"];
        string? cookiesToken = Request.Cookies["g_csrf_token"];

        // Verify the Google Token
        var payload = await VerifyGoogleToken(formCredential, formToken, cookiesToken);
        if (payload == null)
        {
            return BadRequest("Google authorization failed.");
        }

        // Return user info on success
        return Ok(new
        {
            Message = "Google authorization successful",
            Email = payload.Email,
            Name = payload.Name,
            Picture = payload.Picture
        });
    }

    /// <summary>
    /// Verify Google Token
    /// </summary>
    /// <param name="formCredential"></param>
    /// <param name="formToken"></param>
    /// <param name="cookiesToken"></param>
    /// <returns></returns>
    public async Task<GoogleJsonWebSignature.Payload?> VerifyGoogleToken(string? formCredential, string? formToken, string? cookiesToken)
    {
        // Check for null or empty values
        if (string.IsNullOrEmpty(formCredential) || string.IsNullOrEmpty(formToken) || string.IsNullOrEmpty(cookiesToken))
        {
            return null;
        }

        if (formToken != cookiesToken)
        {
            return null;
        }

        GoogleJsonWebSignature.Payload? payload;
        try
        {
            string GoogleApiClientId = _configuration["AuthenticationBuilder:Google:ClientId"];
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { GoogleApiClientId }
            };
            payload = await GoogleJsonWebSignature.ValidateAsync(formCredential, settings);

            // Check issuer
            if (payload?.Issuer != "accounts.google.com" && payload?.Issuer != "https://accounts.google.com")
            {
                return null;
            }

            // Check expiration
            if (payload.ExpirationTimeSeconds != null)
            {
                DateTime now = DateTime.UtcNow;
                DateTime expiration = DateTimeOffset.FromUnixTimeSeconds((long)payload.ExpirationTimeSeconds).UtcDateTime;
                if (now > expiration)
                {
                    return null;
                }
            }
        }
        catch
        {
            return null; // Consider logging the error for debugging
        }
        return payload;
    }
}
