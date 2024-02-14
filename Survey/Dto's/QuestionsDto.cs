using Microsoft.AspNetCore.Mvc.Rendering;
using Survey.Models.Entitites;

namespace Survey.Dto_s
{
	public class QuestionsDto
	{
		public int Id { get; set; }
		public string QuesitonTitle { get; set; }

		public string QuesitonDesciription { get; set; }
		public QuestionType questionType { get; set; }
		public List<SelectListItem> Surveys{ get; set; }
        public int point { get; set; }
        public string correct_Answer { get; set; }//doğru cevap


    }
}
