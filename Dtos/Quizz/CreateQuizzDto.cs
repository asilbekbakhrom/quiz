using System.ComponentModel.DataAnnotations;

namespace quizz.Dtos.Quizz;

public class CreateQuizzDto
{
    [Required, MaxLength(255)]
    public string? Title { get; set; }
    [Required, MaxLength(255)]
    public string? PassWord { get; set; }
    [Required, MaxLength(512)]
    public string? Description { get; set; }
    [Required]
    public DateTimeOffset StartTime { get; set; }
    [Required]
    public DateTimeOffset EndTime { get; set; }

}