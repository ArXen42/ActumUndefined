using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Undefined.Scoring.WebApp.Model;

namespace Undefined.Scoring.WebApp.Controllers
{
	[Route("checkpoints")]
	public class CheckpointsController:Controller
	{
		public async Task<IActionResult> PostCheckpoint([FromBody] CaseCheckpoint checkpoint)
		{
			
			if (checkpoint.Id != default(Int32))
				return BadRequest("Hackaton case ID must be zero, it will be generated automatically.");
			
			using (var db = new HackScoreDbContext())
			{
				await db.Checkpoints.AddAsync(checkpoint);
				await db.SaveChangesAsync();
				return Ok(checkpoint.Id);
			}
		}
	}
}