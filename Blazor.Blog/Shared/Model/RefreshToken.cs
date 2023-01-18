using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Blog.Shared.Model
{
	public class RefreshToken
	{
		[Key]
		public string Token { get; set; } = string.Empty;
		public DateTime Created { get; set; }
		public DateTime Expires { get; set; }
	}
}
