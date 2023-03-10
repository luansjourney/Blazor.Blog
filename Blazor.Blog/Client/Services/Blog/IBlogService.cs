using Blazor.Blog.Shared.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor.Blog.Client.Services.Blog
{
    public interface IBlogService
    {
        //A method that receives all the blog posts
        Task<List<BlogPost>> GetBlogPosts();

        //To receive a single one
        Task<BlogPost> GetBlogPostByUrl(string url);

        //To create a new blog post
        Task<BlogPost> CreateNewPost(BlogPost request);

    }
}
