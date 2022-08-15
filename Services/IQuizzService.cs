using quizz.Models;
using quizz.Models.Quizz;

namespace quizz.Services;

public interface IQuizzService
{
    ValueTask<Result<List<Quizz>>> GetAllQuizzsAsync();
    ValueTask<Result<Quizz>> GetByIdAsync(ulong id);
    ValueTask<Result<Quizz>> RemoveByIdAsync(ulong id);
    ValueTask<Result<Quizz>> CreateAsync(string title, string description,string PassWord,DateTimeOffset startTime, DateTimeOffset endTime);
    ValueTask<Result<Quizz>> UpdateAsync(ulong id, string title, string PassWord,string description,DateTimeOffset startTime, DateTimeOffset endTime);
    ValueTask<bool> ExistsAsync(ulong id);
}