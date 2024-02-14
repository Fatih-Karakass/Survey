using Survey.Models.Entitites;

namespace Survey.Dto_s
{
	public class SurveyStartQuestionDto
	{
		public int Id { get; set; }
		public string QuestionTitle{ get; set; }
		public string answer { get; set; }
		public QuestionType QuestionType { get; set; }
		public QuestionOptions Opt{ get; set; }
		public IFormFile file { get; set; }
		
	}
}
