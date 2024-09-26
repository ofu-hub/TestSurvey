namespace Survey.Domain.Entities;

/// <summary>
/// Модель результата
/// </summary>
public class Result
{
    /// <summary>
    /// Идентификатор результата
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Идентификатор интервью
    /// </summary>
    public Guid InterviewId { get; set; }
    
    /// <summary>
    /// Интервью
    /// </summary>
    public virtual Interview Interview { get; set; } = null!;
    
    /// <summary>
    /// Идентификатор вопроса
    /// </summary>
    public Guid QuestionId { get; set; }
    
    /// <summary>
    /// Идентификатор ответа
    /// </summary>
    public Guid AnswerId { get; set; }
}