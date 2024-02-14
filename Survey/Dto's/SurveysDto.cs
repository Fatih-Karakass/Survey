using Survey.Models.Entitites;

namespace Survey.Dto_s
{
	public class SurveysDto
	{
		public int Id { get; set; }
		//public List<Questions> questions { get; set; }
		public string Name { get; set; }

		public DateTime StartedTime { get; set; }
		public DateTime FinishedTime { get; set; }
	}
}
