using Survey.Models.Entitites;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.Dto_s
{
    public class UpdateSurveysDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Questions> questions { get; set; }
        public List<Questions> questionsAll { get; set; }
        public List<bool> IsSelected{ get; set; }
        public DateTime FinishedTime { get; set; }
    }
}
