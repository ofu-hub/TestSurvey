namespace Survey.Domain.Entities;

/// <summary>
/// Модель ответа
/// </summary>
public class Answer
{
    /// <summary>
    /// Идентификатор ответа
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Текст ответа
    /// </summary>
    public string Text { get; set; } = string.Empty;
    
    /// <summary>
    /// Идентификатор вопроса
    /// </summary>
    public Guid QuestionId { get; set; }
    
    /// <summary>
    /// Вопрос
    /// </summary>
    public virtual Question Question { get; set; } = null!;
}