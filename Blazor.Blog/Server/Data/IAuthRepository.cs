using Blazor.Blog.Shared.Model;
using System.Threading.Tasks;

namespace Blazor.Blog.Server.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string email, string password);
        Task<bool> UserExists(string email);
    }
}
