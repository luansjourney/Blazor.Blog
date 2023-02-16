using Blazor.Blog.Shared.Model;
using System.Threading.Tasks;

namespace Blazor.Blog.Client.Services.Auth
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(UserRegister request);
		Task<ServiceResponse<string>> Login(UserLogin request);
	}
}
