using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Blazor.Blog.Server.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Blog.Server
{
    public class Program
    {
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddRazorPages();
			builder.Services.AddServerSideBlazor();
			builder.Services.AddDbContext<DataContext>(x => x.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
			builder.Services.AddControllersWithViews();
			builder.Services.AddRazorPages();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(options => {
					options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
					{
						Description = "Standard Authorization header using the Bearer scheme(\"bearer {token}\")",
						In = ParameterLocation.Header,
						Name = "Authorization",
						Type = SecuritySchemeType.ApiKey
					});

						options.OperationFilter<SecurityRequirementsOperationFilter>();
			});
			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options => {
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
						.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
						ValidateIssuer = false,
						ValidateAudience = false
					};
					}
				);





			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
				app.UseSwagger();

				// This middleware serves the Swagger documentation UI
				app.UseSwaggerUI();
			}

			// This middleware serves generated Swagger document as a JSON endpoint

			app.UseHttpsRedirection();

			app.UseStaticFiles();

			app.UseRouting();
			app.UseAuthentication();

			app.UseAuthorization();
			app.MapControllers();
			app.MapBlazorHub();
			app.MapRazorPages();
			app.MapFallbackToFile("index.html");

			app.Run();
		}
	}
}
