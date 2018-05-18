using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Undefined.Scoring.WebApp
{
	public static class Program
	{
		public static void Main(String[] args)
		{
			BuildWebHost(args).Run();
		}

		private static IWebHost BuildWebHost(String[] args)
		{
			return WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.UseUrls("http://localhost")
				.Build();
		}
	}
}