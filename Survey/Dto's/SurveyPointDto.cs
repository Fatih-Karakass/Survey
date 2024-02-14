namespace Survey.Dto_s
{
	public class SurveyPointDto
	{
		public int question_Id { get; set; }
		public int User_Id { get; set; }
		public string answer { get; set; }
		public string correct_Answer { get; set; }
		public int Point { get; set; }
	}
}
