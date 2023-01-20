using Blazor.Blog.Shared.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace Blazor.Blog.Client.Services.Auth
{
    public interface IAuthService
    {
        Task<ActionResult<User>> Register(User request);
    }
}
