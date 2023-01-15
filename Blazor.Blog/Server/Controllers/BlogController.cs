using Blazor.Blog.Server.Data;
using Blazor.Blog.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Blog.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BlogController : ControllerBase
	{
		private readonly DataContext _context;
		
		public BlogController(DataContext context)
		{
			_context = context;
		}


		[HttpGet]
		public ActionResult<List<BlogPost>> Get()
		{
			return Ok(_context.BlogPosts.OrderByDescending(post => post.DateCreated));
		}

		[HttpGet("{url}")]
		public ActionResult<BlogPost> Get(string url)
		{
			var post = _context.BlogPosts.FirstOrDefault(p => p.Url.ToLower().Equals(url.ToLower()));

			if(post == null)
			{
				return NotFound("This post does not exist.");
			}

			return Ok(post);
		}

		[HttpPost]
		public async Task<ActionResult<BlogPost>> CreateNewPost(BlogPost request)
		{
			_context.Add(request);
			await _context.SaveChangesAsync();

			return request;
		}
	}
}
