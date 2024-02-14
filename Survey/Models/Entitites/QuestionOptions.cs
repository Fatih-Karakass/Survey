using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.Models.Entitites
{
    public class QuestionOptions
    {
        public int Id { get; set; }
        public Questions Quesiton{ get; set; }
        [ForeignKey(nameof(Quesiton))]
        public int QuesitonId{ get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
    }
}
