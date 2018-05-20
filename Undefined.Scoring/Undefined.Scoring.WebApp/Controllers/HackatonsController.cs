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
	[Route("/api/hackatons")]
	[UsedImplicitly]
	public class HackatonsController : Controller
	{
		[HttpGet]
		[UsedImplicitly]
		public async Task<IEnumerable<Hackaton>> GetHackatons()
		{
			using (var db = new HackScoreDbContext())
			{
				return await db.Hackatons.ToArrayAsync();
			}
		}

		[HttpPost]
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

		[HttpGet("{id}/cases")]
		public async Task<IEnumerable<HackatonCase>> GetHackatonsCases(Int32 id)
		{
			using (var db = new HackScoreDbContext())
			{
				return await db.Cases
					.Where(c => c.Hackaton.Id == id)
					.ToArrayAsync();
			}
		}


		[HttpGet("{id}/teams")]
		public async Task<IEnumerable<Team>> GetCommandsOfHackaton(Int32 id)
		{
			using (var db = new HackScoreDbContext())
			{
				return await db.Teams
					.Where(t => t.HackatonId == id)
					.ToArrayAsync();
			}
		}
	}
}