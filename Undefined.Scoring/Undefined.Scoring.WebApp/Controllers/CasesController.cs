using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Undefined.Scoring.WebApp.Model;

namespace Undefined.Scoring.WebApp.Controllers
{
	[Route("/api/cases")]
	public class CasesController : Controller
	{
		[HttpPost]
		public async Task<IActionResult> PostHackatonCase([FromBody] HackatonCase hackatonCase)
		{
			if (hackatonCase.Id != default(Int32))
				return BadRequest("Hackaton case ID must be zero, it will be generated automatically.");

			using (var db = new HackScoreDbContext())
			{
				await db.Cases.AddAsync(hackatonCase);
				await db.SaveChangesAsync();
				return Ok(hackatonCase.Id);
			}
		}
	}
}