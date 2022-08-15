namespace quizz.Entities;

public class Quizz : EntityBase
{
    public string? Title { get; set; }
    public string? PassWord { get; set; }
    public string? PassWordHash { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }


    [Obsolete("Used only for entity binding.")]
    public Quizz() { }

    public Quizz(string? title, string? passWord, string? description, DateTimeOffset startTime, DateTimeOffset endTime)
    {
        Title = title;
        PassWord = passWord;
        Description = description;
        StartTime = startTime;
        EndTime = endTime;
    }
}