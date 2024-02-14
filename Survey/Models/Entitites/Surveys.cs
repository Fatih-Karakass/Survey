namespace Survey.Models.Entitites
{
	public class Surveys
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<Questions> questions{ get; set; }
		public DateTime StartedTime { get; set; }
		public DateTime FinishedTime { get; set; }
	}
}
