using System;
using System.Collections.Generic;

namespace Undefined.Scoring.WebApp.Model
{
	public class Hackaton
	{
		public Int32 Id { get; set; }

		public virtual IEnumerable<HackatonCase> Cases { get; set; }
	}

	public class HackatonCase
	{
		public Int32 Id { get; set; }

		public virtual Hackaton Hackaton { get; set; }

		public virtual IEnumerable<CaseCheckpoint> Checkpoints { get; set; }
	}

	public class CaseCheckpoint
	{
		public Int32 Id { get; set; }

		public virtual IEnumerable<Criteria> Criterias { get; set; }
	}

	public class Criteria
	{
		public Int32 Id { get; set; }
	}

	public class User
	{
		public Int32 Id { get; set; }

		public virtual Team Team { get; set; }
	}

	public class Team
	{
		public Int32 Id { get; set; }

		public virtual IEnumerable<User> Users { get; set; }
	}

	public class TeamScore
	{
		public Int32 TeamId     { get; set; }
		public Int32 CriteriaId { get; set; }

		public virtual Team     Team     { get; set; }
		public virtual Criteria Criteria { get; set; }
	}
}