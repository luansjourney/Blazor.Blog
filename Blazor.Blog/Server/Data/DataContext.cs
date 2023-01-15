using Blazor.Blog.Shared;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor.Blog.Server.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{
		}

		
		public DbSet<BlogPost> BlogPosts { get; set; }
	}
}
