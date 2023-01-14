using Blazor.Blog.Shared;
using System.Collections.Generic;

namespace Blazor.Blog.Client.Services
{
	public interface IBlogService
	{
		//A method that receives all the blog posts
		List<BlogPost> GetBlogPosts();

		//To receive a single one
		BlogPost GetBlogPostByUrl(string url);

	}
}
