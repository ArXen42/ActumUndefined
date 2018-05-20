using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Undefined.Scoring.WebApp.Model;

namespace Undefined.Scoring.WebApp.Controllers
{
	[Route("api/teams")]
	public class TeamsController : Controller
	{
		public async Task<IActionResult> PostTeam([FromBody] Team team)
		{
			if (team.Id != default(Int32))
				return BadRequest("ID must be zero, it will be generated automatically.");
			
			using (var db = new HackScoreDbContext())
			{
				await db.Teams.AddAsync(team);
				await db.SaveChangesAsync();
			}

			return Ok(team.Id);
		}
	}
}