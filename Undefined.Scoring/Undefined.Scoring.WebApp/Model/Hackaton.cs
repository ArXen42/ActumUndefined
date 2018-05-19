using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Undefined.Scoring.WebApp.Model
{
	[UsedImplicitly]
	public class Hackaton
	{
		public Int32 Id { get; set; }

		public String Name        { get; set; }
		public String Description { get; set; }

		public DateTime BeginDate { get; set; }
		public DateTime EndDate   { get; set; }

		[JsonIgnore]
		public virtual IEnumerable<HackatonCase> Cases { get; set; }
	}

	[UsedImplicitly]
	public class HackatonCase
	{
		public Int32 Id { get; set; }

		public String Name        { get; set; }
		public String Description { get; set; }

		public Int32 HackatonId { get; set; }

		[JsonIgnore]
		public virtual Hackaton Hackaton { get; set; }

		[JsonIgnore]
		public virtual IEnumerable<CaseCheckpoint> Checkpoints { get; set; }
	}

	[UsedImplicitly]
	public class CaseCheckpoint
	{
		public Int32 Id { get; set; }

		public String   Name        { get; set; }
		public String   Description { get; set; }
		public DateTime Deadline    { get; set; }

		[JsonIgnore]
		public virtual IEnumerable<Criteria> Criterias { get; set; }

		public Int32 Scores => Criterias.Sum(c => c.Scores);
	}

	[UsedImplicitly]
	public class Criteria
	{
		public Int32 Id { get; set; }

		public String Name   { get; set; }
		public Int32  Scores { get; set; }

		public Int32 CheckpointId { get; set; }

		[JsonIgnore]
		public virtual CaseCheckpoint Checkpoint { get; set; }
	}

	[UsedImplicitly]
	public class User
	{
		public Int32 Id { get; set; }

		public String UserName { get; set; }
		public String Contacts { get; set; }

		public Int32 TeamId { get; set; }

		[JsonIgnore]
		public virtual Team Team { get; set; }
	}

	[UsedImplicitly]
	public class Team
	{
		public Int32 Id { get; set; }

		public String Name { get; set; }

		public Hackaton HackatonId { get; set; }
		public Int32    CaseId     { get; set; }

		[JsonIgnore]
		public virtual Hackaton Hackaton { get; set; }

		[JsonIgnore]
		public virtual HackatonCase Case { get; set; }

		[JsonIgnore]
		public virtual IEnumerable<User> Users { get; set; }

		[JsonIgnore]
		public virtual IEnumerable<TeamScore> TeamScores { get; set; }
	}

	[UsedImplicitly]
	public class TeamScore
	{
		public Int32 TeamId     { get; set; }
		public Int32 CriteriaId { get; set; }

		[JsonIgnore]
		public virtual Team Team { get; set; }

		[JsonIgnore]
		public virtual Criteria Criteria { get; set; }
	}
}