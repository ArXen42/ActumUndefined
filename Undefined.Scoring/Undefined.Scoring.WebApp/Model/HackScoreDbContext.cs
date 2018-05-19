using Microsoft.EntityFrameworkCore;

namespace Undefined.Scoring.WebApp.Model
{
	public class HackScoreDbContext : DbContext
	{
		public DbSet<Hackaton>       Hackatons   { get; set; }
		public DbSet<HackatonCase>   Cases       { get; set; }
		public DbSet<CaseCheckpoint> Checkpoints { get; set; }
		public DbSet<Criteria>       Criterias   { get; set; }
		public DbSet<User>           Users       { get; set; }
		public DbSet<Team>           Teams       { get; set; }
		public DbSet<TeamScore>      TeamScores  { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder
				.UseNpgsql(@"Server=192.168.43.153;Database=HackScoreDb;User Id=undefined");
//				.UseLazyLoading();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<Hackaton>()
				.Property(h => h.Id)
				.ValueGeneratedOnAdd();

			modelBuilder
				.Entity<HackatonCase>()
				.Property(c => c.Id)
				.ValueGeneratedOnAdd();

			modelBuilder
				.Entity<CaseCheckpoint>()
				.Property(c => c.Id)
				.ValueGeneratedOnAdd();

			modelBuilder
				.Entity<Criteria>()
				.Property(c => c.Id)
				.ValueGeneratedOnAdd();

			modelBuilder
				.Entity<User>()
				.Property(u => u.Id)
				.ValueGeneratedOnAdd();

			modelBuilder
				.Entity<Team>()
				.Property(t => t.Id)
				.ValueGeneratedOnAdd();

			modelBuilder
				.Entity<TeamScore>()
				.HasKey(score => new {score.TeamId, score.CriteriaId});
		}
	}
}