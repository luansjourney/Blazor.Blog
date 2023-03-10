using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Blog.Shared.Model
{
	[Index(nameof(Username),IsUnique = true)]
	public class User
	{
		public int Id { get; set; }
		
		public string Username { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
		public byte[] PasswordSalt { get; set; }	
		public bool IsConfirmed { get; set; }
	}
}
