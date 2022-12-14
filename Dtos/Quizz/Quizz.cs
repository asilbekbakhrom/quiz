namespace quizz.Dtos.Quizz;

public class Quizz
{
    public ulong Id { get; set; }
    public string? Title { get; set; }
    public string? PassWord { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}