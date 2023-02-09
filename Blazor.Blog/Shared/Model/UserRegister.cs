using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Blog.Shared.Model
{
	public class UserRegister
	{
		[Required, EmailAddress]
		public string Email { get; set; }
		[StringLength(16, ErrorMessage = "Your username is too long(16 characters max).")]
		public string Username { get; set; }
		[Required, StringLength(100, MinimumLength = 16)]
		public string Password { get; set; }
		[Compare("Password", ErrorMessage = "The passwors do not match.")]
		public string ConfirmPassword { get; set; }
		[Range(typeof(bool), "true","true", ErrorMessage = "Only confirmed users can blog")]
		public bool IsConfirmed { get; set; } = true;

	}
}
