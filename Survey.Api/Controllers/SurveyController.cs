using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Survey.Api.DTOs.Survey;
using Survey.Domain.Entities;
using Survey.Infrastructure;

namespace Survey.Api.Controllers;

/// <summary>
/// Контроллер опросов
/// </summary>
[ApiController]
[Route("api/surveys")]
public class SurveyController : ControllerBase
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    private readonly DatabaseContext _context;

    /// <summary>
    /// Конструктор контроллера
    /// </summary>
    /// <param name="context"></param>
    public SurveyController(DatabaseContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получение списка опросов
    /// </summary>
    /// <param name="surveyId">Идентификатор опроса</param>
    /// <param name="questionId">Идентификатор вопроса</param>
    /// <returns></returns>
    [HttpGet("{surveyId}/questions/{questionId}")]
    public async Task<IActionResult> GetQuestion(Guid surveyId, Guid questionId)
    {
        // Поиск вопроса по ID опроса и вопроса
        var question = await _context.Questions
            .Include(q => q.Answers) // Включаем ответы
            .FirstOrDefaultAsync(q => q.SurveyId == surveyId && q.Id == questionId);

        if (question == null)
        {
            return NotFound("Вопрос не найден");
        }

        // Формируем объект для возврата данных
        // Можно сделать DTO
        var questionData = new
        {
            question.Id,
            question.Text,
            Answers = question.Answers.Select(a => new
            {
                a.Id,
                a.Text
            }).ToList()
        };

        return Ok(questionData);
    }
    
    /// <summary>
    /// Сохранение ответа пользователя
    /// </summary>
    /// <param name="surveyId">Идентификатор опроса</param>
    /// <param name="questionId">Идентификатор вопроса</param>
    /// <param name="model">Модель отправки ответа на вопрос</param>
    /// <returns></returns>
    [HttpPost("{surveyId}/questions/{questionId}/answers")]
    public async Task<IActionResult> SaveResult(Guid surveyId, Guid questionId, [FromBody] SubmitAnswerRequestDto model)
    {
        // Проверяем, был ли уже сохранен ответ на вопрос
        var isResult = await _context.Results
            .Include(r => r.Interview)
            .Where(r => r.Interview.SurveyId == surveyId)
            .FirstOrDefaultAsync(r => r.InterviewId == model.InterviewId && r.QuestionId == questionId);
        
        if (isResult is not null)
        {
            return BadRequest("Ответ на вопрос уже был сохранен");
        }
        
        // Проверяем, существует ли указанное интервью
        var interview = await _context.Interviews
            .Include(i => i.Results)
            .FirstOrDefaultAsync(i => i.Id == model.InterviewId && i.SurveyId == surveyId);

        if (interview is null)
        {
            return NotFound("Интервью не найдено");
        }

        // Проверяем, существует ли указанный вопрос
        var question = await _context.Questions
            .Include(q => q.Survey)
            .FirstOrDefaultAsync(q => q.SurveyId == surveyId && q.Id == questionId);

        if (question is null)
        {
            return NotFound("Вопрос не найден");
        }

        // Сохраняем результат ответа на вопрос
        var result = new Result
        {
            Id = Guid.NewGuid(),
            InterviewId = interview.Id,
            QuestionId = questionId,
            AnswerId = model.SelectedAnswerId
        };

        _context.Results.Add(result);
        await _context.SaveChangesAsync();

        // Получаем все вопросы опроса
        var allQuestions = await _context.Questions
            .Where(q => q.SurveyId == surveyId)
            .OrderBy(q => q.Id)
            .ToListAsync();

        // Получаем все результаты для данного интервью
        var answeredQuestionIds = interview.Results.Select(r => r.QuestionId).ToList();

        // Находим следующий вопрос, который еще не был отвечен
        var nextQuestion = allQuestions.FirstOrDefault(q => !answeredQuestionIds.Contains(q.Id));

        if (nextQuestion is null)
        {
            // Если вопросов больше нет, завершаем интервью
            interview.EndTime = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            // Возвращаем сообщение о завершении опроса
            // Можно сделать DTO
            return Ok(new
            {
                Message = "Опрос завершен",
                InterviewEnd = true
            });
        }

        // Возвращаем ID следующего вопроса
        // Можно сделать DTO
        return Ok(new
        {
            NextQuestionId = nextQuestion.Id,
            InterviewEnd = false
        });
    }


    /// <summary>
    /// Начало нового интервью
    /// </summary>
    /// <param name="surveyId">Идентификатор опроса</param>
    /// <returns></returns>
    [HttpPost("{surveyId}/interviews")]
    public async Task<IActionResult> StartInterview(Guid surveyId)
    {
        // Проверяем, существует ли указанный опрос
        var survey = await _context.Surveys.FirstOrDefaultAsync(s => s.Id == surveyId);

        if (survey == null)
        {
            return NotFound("Опрос не найден");
        }

        // Создаем новое интервью
        var interview = new Interview
        {
            Id = Guid.NewGuid(),
            SurveyId = surveyId,
            StartTime = DateTime.UtcNow
        };

        _context.Interviews.Add(interview);
        await _context.SaveChangesAsync();

        // Возвращаем ID интервью для дальнейшего использования
        // Можно сделать DTO
        return Ok(new
        {
            InterviewId = interview.Id
        });
    }
}
