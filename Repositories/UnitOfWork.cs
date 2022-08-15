using quizz.Data;

namespace quizz.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public ITopicRepository Topics { get; }
    public IQuizzRepository Quizzs { get; }


    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Topics = new TopicRepository(context);
        Quizzs=  new QuizzRepository(context);

    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public int Save()
        => _context.SaveChanges();
}