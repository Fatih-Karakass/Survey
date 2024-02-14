using AutoMapper;
using Survey.Dto_s;
using Survey.Models.Entitites;

namespace Survey.Mapper
{
	public class SurveyProfile:Profile
	{
		public SurveyProfile()
		{
			CreateMap<QuestionsDto, Questions>().ReverseMap();
			CreateMap<UpdateQuestionDto, Questions>().ReverseMap();
			CreateMap<SurveysDto,Surveys>().ReverseMap();
			CreateMap<UpdateSurveysDto,Surveys>().ReverseMap();
		}
	}
}
