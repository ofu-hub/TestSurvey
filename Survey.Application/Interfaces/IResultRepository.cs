using Survey.Domain.Entities;

namespace Survey.Application.Interfaces;

/// <summary>
/// Интерфейс репозитория результатов
/// </summary>
public interface IResultRepository
{
    /// <summary>
    /// Сохранить результат
    /// </summary>
    /// <param name="result">Результат опроса</param>
    /// <returns></returns>
    Task SaveResult(Result result);
    
    /// <summary>
    /// Получить следующий вопрос
    /// </summary>
    /// <param name="currentQuestionId">Идентификатор текущего вопроса</param>
    /// <param name="surveyId">Идентификатор опроса</param>
    /// <returns></returns>
    Task<Guid?> GetNextQuestionId(Guid currentQuestionId, Guid surveyId);
}