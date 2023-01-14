﻿using Blazor.Blog.Shared;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.Blog.Client.Services
{
	public class BlogService : IBlogService
	{

		public List<BlogPost> Posts { get; set; } = new List<BlogPost>()
		{
			new BlogPost {Url = "new-tutorial", Title = "A new Tutorial about Blazor", Description = "This is a new tutorial...", Content="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."},
			new BlogPost {Url = "first-post" ,Title = "My Second test blog post",	Description = "This is my new blog...", Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum." }
		};
	
	public BlogPost GetBlogPostByUrl(string url)
		{
			return Posts.FirstOrDefault(p => p.Url.ToLower().Equals(url.ToLower()));
		}

		public List<BlogPost> GetBlogPosts()
		{
			return Posts;
		}
	}
}
