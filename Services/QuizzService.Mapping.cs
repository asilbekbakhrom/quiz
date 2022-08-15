using quizz.Models.Quizz;

namespace quizz.Services;

public partial class QuizzService
{
    public static Quizz ToModel(Entities.Quizz entity)
    => new()
    {
        Id = entity.Id,
        PassWord = entity.PassWord,
        CreatedAt = entity.CreatedAt,
        UpdatedAt = entity.UpdatedAt,
        Title = entity.Title,
        Description = entity.Description,
        EndTime = entity.EndTime,
        StartTime = entity.StartTime
    };
}