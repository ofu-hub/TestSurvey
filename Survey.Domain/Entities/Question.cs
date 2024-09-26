namespace Survey.Domain.Entities;

/// <summary>
/// Модель вопроса
/// </summary>
public class Question
{
    /// <summary>
    /// Идентификатор вопроса
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Текст вопроса
    /// </summary>
    public string Text { get; set; } = string.Empty;
    
    /// <summary>
    /// Ответы на вопрос
    /// </summary>
    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();
    
    /// <summary>
    /// Идентификатор опроса
    /// </summary>
    public Guid SurveyId { get; set; }
    
    /// <summary>
    /// Опрос
    /// </summary>
    public virtual Survey Survey { get; set; } = null!;
}