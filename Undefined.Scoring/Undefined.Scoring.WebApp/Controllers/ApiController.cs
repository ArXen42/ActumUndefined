using System;
using Microsoft.AspNetCore.Mvc;
using Undefined.Scoring.WebApp.Model;

namespace Undefined.Scoring.WebApp.Controllers
{
	[Route("api/test")]
	public class ApiController : ControllerBase
	{
		[HttpGet("asd")]
		[Produces("application/json")]
		[AllowCrossSiteJson]
		public ActionResult Get()
		{
			Console.WriteLine("Requested");
			return Ok(new {Value1 = 1, Value2 = 2});
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public String Get(Int32 id)
		{
			return "value";
		}

		// POST api/values
		[HttpPost]
		public void Post([FromBody] String value) { }

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(Int32 id, [FromBody] String value) { }

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(Int32 id) { }
	}
}