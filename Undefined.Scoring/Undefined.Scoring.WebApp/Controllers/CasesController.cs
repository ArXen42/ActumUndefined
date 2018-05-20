using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

		[HttpGet("{id}/checkpoints")]
		public async Task<IEnumerable<CaseCheckpoint>> GetCaseCheckpoints(Int32 id)
		{
			using (var db = new HackScoreDbContext())
			{
				var hackatonCase = await db.Cases
					.FirstAsync(c => c.Id == id);

				return hackatonCase.Checkpoints.ToArray();
			}
		}
		
	}
}