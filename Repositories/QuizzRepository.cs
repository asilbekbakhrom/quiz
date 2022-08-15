using quizz.Data;
using quizz.Entities;

namespace quizz.Repositories;

public class QuizzRepository : GenericRepository<Quizz>, IQuizzRepository
{
    public QuizzRepository(ApplicationDbContext context)
        : base(context) { }
}