using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Blog.Shared.Model
{
	public class UserLogin
	{
		[Required(ErrorMessage = "Please enter an email address.")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Please enter the password.")]
		public string Password { get; set; }
	}
}
