using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Survey.DataAccsess;
using Survey.Dto_s;
using Survey.Function;
using Survey.Models;
using Survey.Models.Entitites;
using System.Diagnostics;

namespace Survey.Controllers
{
	public class HomeController : Controller
	{
		private readonly AppDbContext _context;
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger, AppDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Index()
		{

			return View();
		}
		[HttpPost]
		public IActionResult Surveys(UserDto user)
		{
			var User = _context.User.Find(user.Id);
			var Surveys = _context.Surveys.Include(x => x.questions).ToList();

			var entity = new UserSurveyDto()
			{
				Surveys = Surveys,

				surveyPoints=_context.surveyPoints.Where(x=>x.UserId==user.Id).ToList(),
				User = User
			};



			return View(entity);
		}
		public async Task<IActionResult> SurveyStart(int id, int userid)
		{

			var survey = await _context.Surveys.Include(x => x.questions).ThenInclude(x => x.Options).FirstOrDefaultAsync(x => x.Id == id);
			var entity = new SurveyStartDto();
			entity.SurveyId = id;
			entity.UserId = userid;
			entity.Question = new();
			for (int i = 0; i < survey.questions.Count; i++)
			{
				entity.Question.Add(new()
				{
					Id = survey.questions[i].Id,
					QuestionTitle = survey.questions[i].QuesitonTitle,
					QuestionType = survey.questions[i].questionType,
					Opt = survey.questions[i].Options
				});
			}

			return View(entity);
		}
		[HttpPost]


		public async Task<IActionResult> SurveyStart(SurveyStartDto survey)
		{
			var answers = new List<Answers>();
			var surveyPoint = new List<SurveyPointDto>();
			int point = 0;
			foreach (var item in survey.Question)
			{
				if (item.file != null)
				{
					var result = Fileupload.Fileuploader(item.file, "wwwroot");
					item.answer = result;
				}



				var surveyPointDto = await _context.Questions.Where(x => x.Id == item.Id).Select(x => new SurveyPointDto
				{
					answer = item.answer,
					correct_Answer = x.correct_Answer,
					question_Id = item.Id,
					User_Id = survey.UserId,
					Point = x.point
				}).FirstOrDefaultAsync();
				if (item.answer == surveyPointDto.correct_Answer)
				{
					point = point + surveyPointDto.Point;
				}
				surveyPoint.Add(surveyPointDto);
				answers.Add(new()
				{

					Answer = item.answer,
					questionsId = item.Id,
					UserId = survey.UserId
				});
			}
			var surveyPoints = new SurveyPoint();
			surveyPoints.Point = point;
			surveyPoints.surveysId = survey.SurveyId;
			surveyPoints.UserId = survey.UserId;
			_context.surveyPoints.Add(surveyPoints);
			_context.Answers.AddRange(answers);


			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}



	}
}