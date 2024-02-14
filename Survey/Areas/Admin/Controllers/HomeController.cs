using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Survey.DataAccsess;
using Survey.Dto_s;
using Survey.Models.Entitites;
using Survey.UnitOfWork;
using static System.Net.Mime.MediaTypeNames;

namespace Survey.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class HomeController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly AppDbContext _appDbContext;

		public HomeController(AppDbContext appDbContext, IMapper mapper, IUnitOfWork unitOfWork)
		{
			_appDbContext = appDbContext;
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		public async Task<IActionResult> Index()
		{
			var userPoint = await _appDbContext.surveyPoints
				.Include(x=>x.User)
				.Include(x=>x.surveys)
				.Select(x=>new UserPointDto
			{
				Point=x.Point,
				SurveyName=x.surveys.Name,
				UserName=x.User.Name
			}).ToListAsync();
			
			return View(userPoint);
		}
		public async Task<IActionResult> ListSurveys()
		{
			var survey = await _appDbContext.Surveys.Include(x=>x.questions).ToListAsync();

			return View(survey);
		}
		public IActionResult AddSurveys()
		{
			return View();

		}
		[HttpPost]
		public async Task<IActionResult> AddSurveys(SurveysDto surveys)
		{
			if (ModelState.IsValid) {

				var entity = _mapper.Map<Surveys>(surveys);
				_appDbContext.Entry(entity).State = EntityState.Added;
				await _unitOfWork.SaveChangesAsync();
			return RedirectToAction(nameof(ListSurveys));
			}
			return View(nameof(AddSurveys));
		}
		public async Task<IActionResult> DeletedSurveys(int id)
		{
			var entity=await _appDbContext.Surveys.FindAsync(id);
			if (entity != null)
			{
				_appDbContext.Entry(entity).State = EntityState.Deleted;
				await _unitOfWork.SaveChangesAsync();
			}
			return RedirectToAction(nameof(ListSurveys));
		}
		public async Task<IActionResult> UpdateSurveys(int id)
		{
			var entity = await _appDbContext.Surveys.Include(x => x.questions).FirstOrDefaultAsync(x => x.Id == id);
			if (entity != null)
			{
				var model = _mapper.Map<UpdateSurveysDto>(entity);
				model.questionsAll = _appDbContext.Questions.ToList();
				model.IsSelected = Enumerable.Repeat(false, model.questionsAll.Count).ToList();
				for (int i = 0; i < model.questionsAll.Count; i++)
				{
					if (model.questions.Contains(model.questionsAll[i]))
					{
						model.IsSelected[i] = true;
					}
				}
				//model.questions=_appDbContext.Questions.ToList();
				//bir tane dto oluşturup içerisinde isselected ve soru başlığı koyulmalı daha sonra ise burdaki dto içerisinde o çağrılmalı databse modelinde soru ve anket çoka çok olmalı şu ana kadar doğru çalışıyor.
			
				return View(model);
			}
			throw new Exception("Değer Bulunamadı");
		}
		[HttpPost]
		public async Task<IActionResult> UpdateSurveys(UpdateSurveysDto surveysDto)
		{
			var entity = await _appDbContext.Surveys.Include(x => x.questions).FirstOrDefaultAsync(x=>x.Id==surveysDto.Id);
			var AllQuestion = _appDbContext.Questions.ToList();
			if (entity!=null)
			{
				for(int i = 0; i < AllQuestion.Count; i++)
				{
					if (surveysDto.IsSelected[i])
					{
						entity!.questions.Add(AllQuestion[i]);
					}
				}
				_appDbContext.Entry(entity).State = EntityState.Modified;
				await _unitOfWork.SaveChangesAsync();
				return RedirectToAction(nameof(ListSurveys));

			}
			throw new Exception("Sorular Eklenemedi");
		}

		public async Task<IActionResult> ListQuestion()
		{
			var entitiy = await _appDbContext.Questions.Include(x=>x.surveys).ToListAsync();
			return View(entitiy);
		}
		public async Task<IActionResult> AddQuestion() 
		{
			var entity = new QuestionsDto();
			entity.Surveys = new();
			var surveys = await _appDbContext.Surveys.ToListAsync();
			foreach(var item in surveys)
			{
				entity.Surveys.Add(new SelectListItem
				{
					Value = item.Id.ToString(),
					Text = item.Name
				});
			}
			return View(entity);
		}
		[HttpPost]
		public async Task<IActionResult> AddQuestion(QuestionsDto questions,string survey)
		{
			
				var entity=_mapper.Map<Questions>(questions);
				entity.surveys = await _appDbContext.Surveys.FindAsync(Convert.ToInt32(survey));
				_appDbContext.Entry(entity).State = EntityState.Added;
				await _unitOfWork.SaveChangesAsync();
				return RedirectToAction(nameof(ListQuestion));

		}

		public async Task<IActionResult> DeletedQuestions(int id)
		{
			var entity = await _appDbContext.Questions.FindAsync(id);
			if (entity != null)
			{
				_appDbContext.Entry(entity).State = EntityState.Deleted;
				await _unitOfWork.SaveChangesAsync();
				return RedirectToAction(nameof(ListQuestion));
			}
			throw new Exception("Bir Hata Meydana Geldi");

        }

		public async Task<IActionResult> UpdateQuestions( int id)
		{
			var entity = await _appDbContext.Questions.FindAsync(id);
         

            var model =_mapper.Map<UpdateQuestionDto>(entity);
            model.Surveys = new();
            var surveys = _appDbContext.Surveys.ToList();
            foreach (var item in surveys)
            {
                model.Surveys.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                });
            }
            return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> UpdateQuestions(UpdateQuestionDto questionDto , string survey)
		{
			var entity = _mapper.Map<Questions>(questionDto);
			entity.surveysId = Convert.ToInt32(survey);
			_appDbContext.Entry(entity).State = EntityState.Modified;
			await _unitOfWork.SaveChangesAsync();

			return RedirectToAction(nameof(ListQuestion));
        }
		public async Task<IActionResult> QuestionOptions(int id)
		{
			var model = await _appDbContext.Questions.Include(x=>x.Options).FirstOrDefaultAsync(z=>z.Id==id);
                TempData["question"] = id;

            if (model.Options == null)
			{
                var entity = new QuestionOptions();
				TempData["State"] = true;//yeni oluştur
                return View(entity);
            }
            TempData["State"] = false;//Güncelle

            return View(model.Options);
			
		}
		[HttpPost]
		public async Task< IActionResult >QuestionOptions(QuestionOptions options) 
		{
			int questionId = Convert.ToInt32( TempData["question"]);
            options.QuesitonId = questionId;
			bool state = Convert.ToBoolean( TempData["State"] );
			if (state == true)
			{
				options.Id = 0;	
				_appDbContext.QuestionOptions.Add(options);
			}
			if (state == false)
			{
				_appDbContext.QuestionOptions.Update(options);
			}
			await _unitOfWork.SaveChangesAsync();

			return RedirectToAction(nameof(ListQuestion));
		}
		public IActionResult User()
		{

			return View();
		}
		[HttpPost]
		public async Task<IActionResult> User(User user)
		{
			_appDbContext.User.Add(user);
			await _appDbContext.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}

	}
}
