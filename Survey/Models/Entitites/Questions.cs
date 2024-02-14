using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.Models.Entitites
{
	public class Questions
	{
		public int Id { get; set; }
		public string QuesitonTitle { get; set; }
		public string QuesitonDesciription { get; set; }
		public QuestionType questionType { get; set; }
		//soru şıkları eklenecek ayrı tabloda yapılabilir
		public Surveys surveys { get; set; }
		[ForeignKey(nameof(surveys))]
		public int surveysId { get; set; }
        public int point { get; set; }
        public string correct_Answer { get; set; }//doğru cevap
		public QuestionOptions Options { get; set; }
		


	}
}
