using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Undefined.Scoring.WebApp.Model;

namespace Undefined.Scoring.WebApp.Controllers
{
	[Route("/api/users")]
	public class UsersController : Controller
	{
		[HttpGet]
		public async Task<IEnumerable<User>> GetUsers()
		{
			using (var db = new HackScoreDbContext())
			{
				return await db.Users.ToArrayAsync();
			}
		}

		[HttpPost]
		public async Task<IActionResult> PostUser([FromBody] User user)
		{
			if (user.Id != default(Int32))
				return BadRequest("ID must be zero, it will be generated automatically.");

			using (var db = new HackScoreDbContext())
			{
				await db.Users.AddAsync(user);
				await db.SaveChangesAsync();
			}

			return Ok(user.Id);
		}

		[HttpPut("{userId}joinTeam/{teamId}")]
		public async Task<IActionResult> UserJoinTeam(Int32 userId, Int32 teamId)
		{
			using (var db = new HackScoreDbContext())
			{
				var user = await db.Users.FirstAsync(u => u.Id == userId);

				user.TeamId = teamId;
				await db.SaveChangesAsync();

				return Ok();
			}
		}
	}
}