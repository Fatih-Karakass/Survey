using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.Models.Entitites
{
	public class Answers
	{
		public int Id { get; set; }
		public User User { get; set; }
		public int UserId { get; set; }
		public Questions questions { get; set; }
		[ForeignKey(nameof(questions))]
		public int questionsId { get; set; }
		public string Answer { get; set; }

	}
}
