using Survey.Models.Entitites;

namespace Survey.Dto_s
{
    public class UserSurveyDto
    {
        public User User{ get; set; }
        public List<SurveyPoint> surveyPoints{ get; set; }
        public List<Surveys> Surveys { get; set;}
    }
}
