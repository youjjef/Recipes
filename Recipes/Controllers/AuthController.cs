using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using RecipeApi;
using RecipeAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Identity.Core;
using ServiceStack.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly Context _context;
    private readonly IConfiguration _config;

    // private readonly object DataContext;

    public AuthController(Context context, IConfiguration config)
    {
        this._context =context;
        _config = config;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(User user)
    {
        // Check if the username or email is already taken
        if (await _context.Users.AnyAsync(u => u.username == user.username || u.email == user.email))
        {
            return BadRequest("Username or Email is already taken.");
        }

        // Hash the password
        // You can use a library like BCrypt.Net or Argon2 for password hashing
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.password);
        // Save the user to the database
        user.password = hashedPassword;
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok("Registration successful.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(User user)
    {
        // Retrieve the user from the database based on the provided username/email
        User existingUser = await _context.Users.FirstOrDefaultAsync(u => u.username == user.username || u.email == user.username);

        if (existingUser == null)
        {
            return BadRequest("Invalid username or password.");
        }

        // Verify the password
        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(user.password, existingUser.password);
        if (!isPasswordValid)
        {
            return BadRequest("Invalid username or password.");
        }

        // Create the authentication token (JWT)
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config["Jwt:SecretKey"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Surname, existingUser.userId.ToString())
            }),
            Expires = DateTime.UtcNow.AddDays(7), // Token expiration time
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        string encodedToken = tokenHandler.WriteToken(token);

        return Ok(new { Token = encodedToken });
    }
}