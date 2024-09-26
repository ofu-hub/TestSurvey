namespace Survey.Domain.Entities;

/// <summary>
/// Модель интервью
/// </summary>
public class Interview
{
    /// <summary>
    /// Идентификатор интервью
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Время начала интервью
    /// </summary>
    public DateTime StartTime { get; set; }
    
    /// <summary>
    /// Время окончания интервью
    /// </summary>
    public DateTime? EndTime { get; set; }
    
    /// <summary>
    /// Результаты опроса
    /// </summary>
    public virtual ICollection<Result> Results { get; set; } = new List<Result>();
    
    /// <summary>
    /// Идентификатор опроса
    /// </summary>
    public Guid SurveyId { get; set; }
    
    /// <summary>
    /// Ответ
    /// </summary>
    public virtual Survey Survey { get; set; } = null!;
}