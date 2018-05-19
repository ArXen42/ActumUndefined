using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Rest;
using Undefined.Scoring.WebApp.Model;

namespace Undefined.Scoring.WebApp.Controllers
{
	[Route("/api")]
	[UsedImplicitly]
	public class ApiController : Controller
	{
		[HttpGet("hackatons")]
		[UsedImplicitly]
		public IEnumerable<Hackaton> GetHackatons()
		{
			using (var db = new HackScoreDbContext())
			{
				return db.Hackatons.ToArray();
			}
		}

		[HttpPost("hackatons")]
		[UsedImplicitly]
		public Int32 PostHackaton([FromBody] Hackaton hackaton)
		{
			using (var db = new HackScoreDbContext())
			{
				if (hackaton.Id != default(Int32))
					throw new HttpOperationException("Hackaton ID must be zero, it will be generated automatically.");

				db.Hackatons.Add(hackaton);
				db.SaveChanges();
				return hackaton.Id;
			}
		}

		[HttpGet("hackatons/{id}/cases")]
		public IEnumerable<HackatonCase> GetHackatonsCases(Int32 id)
		{
			using (var db = new HackScoreDbContext())
			{
				return db.Cases
					.Where(c => c.Hackaton.Id == id)
					.ToArray();
			}
		}

		[HttpPost("hackatons/{id}/cases")]
		public Int32 PostHackatonCase(Int32 id, [FromBody] HackatonCase hackatonCase)
		{
			if (hackatonCase.Id != default(Int32))
				throw new HttpOperationException("Hackaton case ID must be zero, it will be generated automatically.");

			using (var db = new HackScoreDbContext())
			{
				hackatonCase.HackatonId = id;
				db.Cases.Add(hackatonCase);
				db.SaveChanges();
				return hackatonCase.Id;
			}
		}
	}
}