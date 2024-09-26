using Survey.Domain.Entities;

namespace Survey.Application.Interfaces;

/// <summary>
/// Интерфейс репозитория вопросов
/// </summary>
public interface IQuestionRepository
{
    /// <summary>
    /// Получить вопрос по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <returns></returns>
    Task<Question?> GetQuestionById(Guid id);
}