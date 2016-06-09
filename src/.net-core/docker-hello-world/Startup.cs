using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace NetCoreOnDocker
{
	public class Startup
	{
		public void Configure(IApplicationBuilder app)
		{
			app.Run(context =>
			{
				return context.Response.WriteAsync("Hello from ASP.NET Core!");
			});
		}
	}
}