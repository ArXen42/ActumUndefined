using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Undefined.Scoring.WebApp.Model;

namespace Undefined.Scoring.WebApp.Controllers
{
	[Route("/api")]
	[UsedImplicitly]
	public class ApiController : Controller
	{
		[HttpGet("hackatons")]
		[UsedImplicitly]
		public async Task<IEnumerable<Hackaton>> GetHackatons()
		{
			using (var db = new HackScoreDbContext())
			{
				return await db.Hackatons.ToArrayAsync();
			}
		}

		[HttpPost("hackatons")]
		[UsedImplicitly]
		public async Task<IActionResult> PostHackaton([FromBody] Hackaton hackaton)
		{
			using (var db = new HackScoreDbContext())
			{
				if (hackaton.Id != default(Int32))
					return BadRequest("Hackaton ID must be zero, it will be generated automatically.");

				await db.Hackatons.AddAsync(hackaton);
				await db.SaveChangesAsync();

				return Ok(hackaton.Id);
			}
		}

		[HttpGet("hackatons/{id}/cases")]
		public async Task<IEnumerable<HackatonCase>> GetHackatonsCases(Int32 id)
		{
			using (var db = new HackScoreDbContext())
			{
				return await db.Cases
					.Where(c => c.Hackaton.Id == id)
					.ToArrayAsync();
			}
		}

		[HttpPost("hackatons/{id}/cases")]
		public async Task<IActionResult> PostHackatonCase(Int32 id, [FromBody] HackatonCase hackatonCase)
		{
			if (hackatonCase.Id != default(Int32))
				return BadRequest("Hackaton case ID must be zero, it will be generated automatically.");

			using (var db = new HackScoreDbContext())
			{
				hackatonCase.HackatonId = id;
				await db.Cases.AddAsync(hackatonCase);
				await db.SaveChangesAsync();
				return Ok(hackatonCase.Id);
			}
		}
	}
}