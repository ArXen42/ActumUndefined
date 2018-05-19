using System;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Undefined.Scoring.WebApp.Model;

namespace Undefined.Scoring.WebApp.Controllers
{
	[Route("/api")]
	[UsedImplicitly]
	public class ApiController : ControllerBase
	{
		[HttpGet("hackatons")]
		[UsedImplicitly]
		public ActionResult GetHackatons()
		{
			using (var db = new HackScoreDbContext())
			{
				return Ok(db.Hackatons.ToArray());
			}
		}

		[HttpPost("hackatons")]
		[UsedImplicitly]
		public ActionResult PostHackaton([FromBody] Hackaton hackaton)
		{
			using (var db = new HackScoreDbContext())
			{
				if (hackaton.Id != default(Int32))
					return new BadRequestResult();

				db.Hackatons.Add(hackaton);
				db.SaveChanges();
				return Ok(hackaton.Id);
			}
		}

		[HttpGet("hackatons/{id}/cases")]
		public ActionResult GetHackatonsCases(Int32 id)
		{
			using (var db = new HackScoreDbContext())
			{
				return Ok(db
					.Cases
					.Where(c => c.Hackaton.Id == id)
					.ToArray());
			}
		}

		[HttpPost("hackatons/{id}/cases")]
		public ActionResult PostHackatonCase(Int32 id, [FromBody] HackatonCase hackatonCase)
		{
			if (hackatonCase.Id != default(Int32))
				return new BadRequestResult();

			using (var db = new HackScoreDbContext())
			{
				hackatonCase.HackatonId = id;
				db.Cases.Add(hackatonCase);
				db.SaveChanges();
				return Ok(hackatonCase.Id);
			}
		}
	}
}