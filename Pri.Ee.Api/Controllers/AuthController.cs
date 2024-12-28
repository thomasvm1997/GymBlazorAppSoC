using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Pri.Ee.Api.Dtos.Users;
using Pri.EindOpdracht.Core.Entities;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pri.Ee.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginUserRequestDto login)
        {
            var applicationUser = await _userManager.FindByEmailAsync(login.Email);
            var result = await _signInManager.PasswordSignInAsync(applicationUser, login.Password, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            JwtSecurityToken token = await GenerateTokenAsync(applicationUser);
            //defined
            string serializedToken = new JwtSecurityTokenHandler().WriteToken(token); //serialize the token
            return Ok(new LoginUserResponseDto()
            {
                Token = serializedToken
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterUserRequestDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUser = new ApplicationUser
            {
                UserName = registerDto.Username,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                BirthDate = registerDto.BirthDate, // Add BirthDate to ApplicationUser if it's not already there
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var result = await _userManager.CreateAsync(newUser, registerDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new RegisterUserResponseDto
                {
                    Success = false,
                    Errors = result.Errors.Select(e => e.Description).ToList()
                });
            }

            await _userManager.AddToRoleAsync(newUser, "Customer");

            return Ok(new RegisterUserResponseDto
            {
                Success = true,
                Message = "User registered successfully."
            });
        }

        private async Task<JwtSecurityToken> GenerateTokenAsync(ApplicationUser user)
        {
            var claims = new List<Claim>();

            // Loading the user Claims
            var userClaims = await _userManager.GetClaimsAsync(user);

            claims.AddRange(userClaims);

            // Loading the roles and put them in a claim of a Role ClaimType
            var roleClaims = await _userManager.GetRolesAsync(user);
            foreach (var roleClaim in roleClaims)
            {
                claims.Add(new Claim(ClaimTypes.Role, roleClaim));
            }

            // Deze kunnen we later nog gebruiken om mss een profile pic toe te voegen
            //claims.Add(new Claim("ProfileImage", new Uri($"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/img/users/default.jpg").ToString()));

            var expirationDays = _configuration.GetValue<int>("JWTConfiguration:TokenExpirationDays");
            var siginingKey = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWTConfiguration:SigningKey"));
            var token = new JwtSecurityToken
            (
                issuer: _configuration.GetValue<string>("JWTConfiguration:Issuer"),
                audience: _configuration.GetValue<string>("JWTConfiguration:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(expirationDays)),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(siginingKey), SecurityAlgorithms.HmacSha256)
            );

            return token;
        }
    }
}
