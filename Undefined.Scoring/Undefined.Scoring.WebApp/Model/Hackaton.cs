using System;
using System.Collections.Generic;
using System.Linq;

namespace Undefined.Scoring.WebApp.Model
{
	public class Hackaton
	{
		public Int32 Id { get; set; }

		public virtual IEnumerable<HackatonCase> HackatonCases { get; set; }
	}

	public class HackatonCase
	{
		public Int32 Id { get; set; }

		public String Name        { get; set; }
		public String Description { get; set; }

		public virtual Hackaton Hackaton { get; set; }

		public virtual IEnumerable<CaseCheckpoint> Checkpoints { get; set; }
	}

	public class CaseCheckpoint
	{
		public Int32 Id { get; set; }

		public String   Name        { get; set; }
		public String   Description { get; set; }
		public DateTime Deadline    { get; set; }

		public Int32 FinishBeforeDeadlineBonus => 10;

		public Int32 Scores => Criterias.Sum(c => c.Scores);

		public virtual IEnumerable<Criteria> Criterias { get; set; }
	}

	public class Criteria
	{
		public Int32 Id { get; set; }

		public         String         Name       { get; set; }
		public         Int32          Scores     { get; set; }
		public virtual CaseCheckpoint Checkpoint { get; set; }
	}

	public class User
	{
		public Int32 Id { get; set; }

		public String UserName { get; set; }
		public String Contacts { get; set; }

		public virtual Team Team { get; set; }
	}

	public class Team
	{
		public Int32 Id { get; set; }

		public String Name { get; set; }

		public virtual IEnumerable<User>      Users        { get; set; }
		public virtual IEnumerable<TeamScore> TeamScores   { get; set; }
		public virtual Hackaton               Hackaton     { get; set; }
		public virtual HackatonCase           HackatonCase { get; set; }
	}

	public class TeamScore
	{
		public Int32   TeamId     { get; set; }
		public Int32   CriteriaId { get; set; }
		public Boolean Checked    { get; set; }

		public virtual Team     Team     { get; set; }
		public virtual Criteria Criteria { get; set; }
	}
}