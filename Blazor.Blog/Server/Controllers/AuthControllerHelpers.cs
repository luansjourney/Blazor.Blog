using Blazor.Blog.Shared.Model;
using System;
using System.Linq;
using System.Security.Cryptography;

internal static class AuthControllerHelpers
{

	public static User user = new();

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