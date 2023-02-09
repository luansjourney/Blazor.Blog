using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blazor.Blog.Client
{
	public class CustomAuthStateProvider : AuthenticationStateProvider
	{
		public override Task<AuthenticationState> GetAuthenticationStateAsync()
		{

			var identity = new ClaimsIdentity(new[]
			{
				new Claim(ClaimTypes.Name, "Luan")
			}, "test authentication type");

			var user = new ClaimsPrincipal(identity);

			return Task.FromResult(new AuthenticationState(user));
		}
	}
}
