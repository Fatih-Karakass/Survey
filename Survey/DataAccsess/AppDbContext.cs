using Microsoft.EntityFrameworkCore;
using Survey.Models.Entitites;

namespace Survey.DataAccsess
{
	public class AppDbContext:DbContext
	{
		public DbSet<User> User { get; set; }
		public DbSet<Surveys> Surveys{ get; set; }
		public DbSet<SurveyPoint> surveyPoints{ get; set; }
		public DbSet<Questions> Questions{ get; set; }
		public DbSet<Answers> Answers{ get; set; }
		public DbSet<QuestionOptions> QuestionOptions{ get; set; }
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}
	}
}
