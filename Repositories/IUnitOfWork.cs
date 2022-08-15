namespace quizz.Repositories;

public interface IUnitOfWork : IDisposable
{
    ITopicRepository Topics { get; }
    IQuizzRepository Quizzs { get; }

    int Save();
}