using Microsoft.EntityFrameworkCore;
using quizz.Models;
using quizz.Models.Quizz;
using quizz.Repositories;
using quizz.Utils;

namespace quizz.Services;

public partial class QuizzService : IQuizzService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<QuizzService> _logger;

    public QuizzService(IUnitOfWork unitOfWork, ILogger<QuizzService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async ValueTask<bool> ExistsAsync(ulong id)
    {
        var quizzResult = await GetByIdAsync(id);
        return quizzResult.IsSuccess;
    }

    public async ValueTask<Result<Quizz>> CreateAsync(string name, string description,string passWord,DateTimeOffset startTime,DateTimeOffset endTime)
    {
        if(string.IsNullOrWhiteSpace(name))
            return new("Name is invalid.");
        
        if(string.IsNullOrWhiteSpace(description))
            return new("Description is invalid");
        
        if(string.IsNullOrWhiteSpace(passWord))
            return new("Password is invalid");
        
        var quizzEntity = new Entities.Quizz(name, description,passWord,startTime,endTime);

        try
        {
            var createdquizz = await _unitOfWork.Quizzs.AddAsync(quizzEntity);
            return new(true) { Data = ToModel(createdquizz) }; 
        }
        catch(DbUpdateException dbUpdateException)
        {
            _logger.LogInformation("Error occured:", dbUpdateException);
            return new("quizz already exists.");
        }
        catch(Exception e)
        {
            _logger.LogError($"Error occured at {nameof(QuizzService)}", e);
            throw new("Couldn't create quizz. Contact support.", e);
        }
    }

    public async ValueTask<Result<List<Quizz>>> GetAllQuizzsAsync()
    {
        try
        {
            var existingQuizzs = _unitOfWork.Quizzs.GetAll();
            if(existingQuizzs is null)
                return new("No Quizzs found. Contact support.");

            var filteredQuizzs = await existingQuizzs
                .Select(e => ToModel(e))
                .ToListAsync();

            return new(true) { Data = filteredQuizzs };
        }
        catch(Exception e)
        {
            _logger.LogError($"Error occured at {nameof(QuizzService)}", e);
            throw new("Couldn't get Quizzs. Contact support.", e);
        }
    }

    public async ValueTask<Result<Quizz>> GetByIdAsync(ulong id)
    {
        try
        {
            var existingQuizz = await _unitOfWork.Quizzs.GetAll().FirstOrDefaultAsync(t => t.Id == id);
            if(existingQuizz is null)
                return new("Quizz with given ID not found.");

            return new(true) { Data = ToModel(existingQuizz) };
        }
        catch(Exception e)
        {
            _logger.LogError($"Error occured at {nameof(QuizzService)}", e);
            throw new("Couldn't get Quizz. Contact support.", e);
        }
    }

    public async ValueTask<Result<Quizz>> RemoveByIdAsync(ulong id)
    {
        try
        {
            var existingQuizz = _unitOfWork.Quizzs.GetById(id);
            if(existingQuizz is null)
                return new("Quizz with given ID not found.");

            var removedQuizz = await _unitOfWork.Quizzs.Remove(existingQuizz);
            if(removedQuizz is null)
                return new("Removing the Quizz failed. Contact support.");

            return new(true) { Data = ToModel(removedQuizz) };
        }
        catch(Exception e)
        {
            _logger.LogError($"Error occured at {nameof(QuizzService)}", e);
            throw new("Couldn't remove Quizz. Contact support.", e);
        }
    }

    public async ValueTask<Result<Quizz>> UpdateAsync(ulong id, string name,string passWord,string description,DateTimeOffset startTime, DateTimeOffset endTime)
    {
        if(string.IsNullOrWhiteSpace(name))
            return new("Name is invalid.");

        if(string.IsNullOrWhiteSpace(description))
            return new("Description is invalid");

        if(string.IsNullOrWhiteSpace(passWord))
            return new("PassWord is invalid");

        var existingQuizz = _unitOfWork.Quizzs.GetById(id);
        if(existingQuizz is null)
            return new("Quizz with given ID not found.");

        existingQuizz.Title = name;
        existingQuizz.PassWord = passWord;
        existingQuizz.Description = description;

        try
        {
            var updatedQuizz = await _unitOfWork.Quizzs.Update(existingQuizz);
            return new(true) { Data = ToModel(updatedQuizz) };
        }
        catch(DbUpdateException dbUpdateException)
        {
            _logger.LogInformation("Error occured:", dbUpdateException);
            return new("Quizz name already exists.");
        }
        catch(Exception e)
        {
            _logger.LogError($"Error occured at {nameof(QuizzService)}", e);
            throw new("Couldn't update Quizz. Contact support.", e);
        }
    }

}