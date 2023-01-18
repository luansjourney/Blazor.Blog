using Blazor.Blog.Server.Auth;
using Blazor.Blog.Server.Data;
using Blazor.Blog.Shared.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using SymmetricSecurityKey = Microsoft.IdentityModel.Tokens.SymmetricSecurityKey;
using Blazor.Blog.Server.Services.UserService;
using System.Diagnostics.CodeAnalysis;
using System.CodeDom.Compiler;

namespace Blazor.Blog.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{

		[SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "Don't apply")]
		public static User user = new();
		private readonly IConfiguration _configuration;
		private readonly DataContext _context;
		private readonly IUserService _userService;

		public AuthController(IConfiguration configuration, DataContext context, IUserService userService)
		{
			_configuration = configuration;
			_context = context;
			_userService = userService;
		}

		[HttpGet, Authorize]
		public ActionResult<string> GetMe()
		{
			var userName = _userService.GetMyName();
			return Ok(userName);
		}


		[HttpPost("register")]
		public async Task<ActionResult<User>> Register(UserDto request)
		{
			CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

			user.Username = request.Username;
			user.PasswordHash = passwordHash;
			user.PasswordSalt = passwordSalt;

			_context.Add(user);
			await _context.SaveChangesAsync();

			return Ok(user);
		}

		[HttpPost("login")]
		public async Task<ActionResult<string>> Login(UserDto request)
		{
			var loginUser = _context.Users.FirstOrDefault(u => u.Username == request.Username);

			if (loginUser == null)
			{
				return await Task.FromResult(BadRequest("User not found."));
			}
			
			if(!VerifyPasswordHash(request.Password, loginUser.PasswordHash, loginUser.PasswordSalt))
			{
				return await Task.FromResult(BadRequest("Wrong password."));
			}

			string token = CreateToken(loginUser);
			var refreshToken = GenerateRefreshToken();
			SetRefreshToken(refreshToken);
			return await Task.FromResult(Ok(token));
		}

		[HttpPost("refresh-token")]
		public async Task<ActionResult<string>> RefreshToken()
		{
			var refreshToken = Request.Cookies["refreshToken"];

			if (!user.RefreshToken.Equals(refreshToken))
			{
				return await Task.FromResult(Unauthorized("Inavlid referesh token."));
			}
			else if(user.TokenExpires < DateTime.Now)
			{
				return await Task.FromResult(Unauthorized("Token expired."));
			}

			string token = CreateToken(user);
			var newRefreshToken = GenerateRefreshToken();
			SetRefreshToken(newRefreshToken);

			return await Task.FromResult(Ok(token));

		}


		private static RefreshToken GenerateRefreshToken()
		{
			var refreshToken = new RefreshToken
			{
				Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
				Expires = DateTime.Now.AddDays(7),
				Created = DateTime.Now
			};

			return refreshToken;
		}

		private void SetRefreshToken(RefreshToken newRefreshToken)
		{
			var cookieOptions = new CookieOptions
			{
				HttpOnly = true,
				Expires = newRefreshToken.Expires
			};
			Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

			user.RefreshToken = newRefreshToken.Token;
			user.TokenCreated = newRefreshToken.Created;
			user.TokenExpires = newRefreshToken.Expires;
		}

		private string CreateToken(User user)
		{
			//claims
			List<Claim> claims = new()
			{
				new Claim(ClaimTypes.Name, user.Username),
				new Claim(ClaimTypes.Role, "Admin")
			};

			var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.Now.AddDays(1),
				signingCredentials: creds
				);

			var jwt = new JwtSecurityTokenHandler().WriteToken(token);

			return jwt;
		}

		private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using var hmac = new HMACSHA512();
			passwordSalt = hmac.Key;
			passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
		}

		private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
		{
			using var hmac = new HMACSHA512(passwordSalt);
			var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			return computedHash.SequenceEqual(passwordHash);
		}
	}
}
