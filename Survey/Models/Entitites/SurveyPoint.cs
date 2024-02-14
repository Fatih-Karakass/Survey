using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.Models.Entitites
{
	public class SurveyPoint
	{
		public int Id { get; set; }
		public Surveys surveys { get; set; }
		[ForeignKey(nameof(surveys))]
		public int surveysId { get; set; }
		public User User { get; set; }
		public int UserId { get; set; }
		public int Point { get; set; }
	}
}
