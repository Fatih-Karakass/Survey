using Survey.Models.Entitites;

namespace Survey.Dto_s
{
	public class SurveyStartDto
	{
		//public Surveys surveys{ get; set; }
		//public List<Answers> answers { get; set; }
		//public int UserId { get; set; }
		public int UserId { get; set; }
		public int SurveyId { get; set; }
		public List<SurveyStartQuestionDto> Question { get; set; }
	}
}
