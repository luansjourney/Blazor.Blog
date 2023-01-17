using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Blazor.Blog.Server.Services.UserService
{
	public class UserService : IUserService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public UserService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public string GetMyName()
		{
			var result = string.Empty.ToLower();
			if (_httpContextAccessor != null)
			{
				result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
			}

			return result;
		}
	}
}
