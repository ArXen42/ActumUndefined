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
		public ActionResult PostHackaton(String name, String description)
		{
			using (var db = new HackScoreDbContext())
			{
				var hackaton = new Hackaton(name, description);
				db.Hackatons.Add(hackaton);
				db.SaveChanges();
				return Ok(hackaton.Id);
			}
		}
	}
}