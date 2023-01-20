using Blazor.Blog.Shared.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Blazor.Blog.Client.Services.Auth
{
	public class AuthService : IAuthService
	{
		private readonly HttpClient _http;

		public AuthService(HttpClient http)
		{
			_http = http;
		}

		public async Task<ActionResult<User>> Register(User request)
		{
			var result = await _http.PostAsJsonAsync("api/auth/register", request);

			return await result.Content.ReadFromJsonAsync<ActionResult<User>>();
		}
	}
}
