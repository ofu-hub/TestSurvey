namespace Survey.Domain.Entities;

/// <summary>
/// Модель опроса
/// </summary>
public class Survey
{
    /// <summary>
    /// Идентификатор опроса
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Название опроса
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Описание опроса
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Вопросы опроса
    /// </summary>
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
    
    /// <summary>
    /// Список интервью по опросу
    /// </summary>
    public virtual ICollection<Interview> Interviews { get; set; } = new List<Interview>();
}