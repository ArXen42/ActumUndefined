using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Undefined.Scoring.WebApp.Model;

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
			using (var db = new HackScoreDbContext())
			{
				Console.WriteLine("Ensure deleted");
				db.Database.EnsureDeleted();

				Console.WriteLine("Ensure created");
				db.Database.EnsureCreated();
			}

			return WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.UseUrls("http://localhost;http://192.168.43.111")
				.Build();
		}
	}
}