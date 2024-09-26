namespace Survey.Api.DTOs.Survey;

/// <summary>
/// DTO для отправки ответа на вопрос
/// </summary>
public class SubmitAnswerRequestDto
{
    /// <summary>
    /// Идентификатор интервью
    /// </summary>
    public Guid InterviewId { get; set; }
    
    /// <summary>
    /// Идентификатор вопроса
    /// </summary>
    public Guid SelectedAnswerId { get; set; }
}