using Blazor.Blog.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.Blog.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BlogController : ControllerBase
	{

		public List<BlogPost> Posts { get; set; } = new List<BlogPost>()
		{
			new BlogPost {Url = "new-tutorial", Title = "A new Tutorial about Blazor with web API", Description = "This is a new tutorial...", Content="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."},
			new BlogPost {Url = "first-post" ,Title = "My Second test blog post with web API",   Description = "This is my new blog...", Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum." }
		};

		[HttpGet]
		public ActionResult<List<BlogPost>> Get()
		{
			return Ok(Posts);
		}

		[HttpGet("{url}")]
		public ActionResult<BlogPost> Get(string url)
		{
			var post = Posts.FirstOrDefault(p => p.Url.ToLower().Equals(url.ToLower()));

			if(post == null)
			{
				return NotFound("This post does not exist.");
			}

			return Ok(post);
		}

	}
}
