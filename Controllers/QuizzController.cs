using Microsoft.AspNetCore.Mvc;
using quizz.Dtos;
using quizz.Dtos.Quizz;
using quizz.Services;

namespace quizz.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuizzsController : ControllerBase
{
    private readonly ILogger<QuizzsController> _logger;
    private readonly IQuizzService _quizzService;

    public QuizzsController(
        IQuizzService quizzService,
        ILogger<QuizzsController> logger)
    {
        _logger = logger;
        _quizzService = quizzService;
    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Quizz>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
    public async Task<IActionResult> GetAllQuizzs()
    {
        try
        {
            var quizzsResult = await _quizzService.GetAllQuizzsAsync();
            if(!quizzsResult.IsSuccess)
                return NotFound(new { ErrorMessage = quizzsResult.ErrorMessage });

            return Ok(quizzsResult?.Data?.Select(ToDto));

        }
        catch(Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { ErrorMessage = e.Message });
        }
    }

    [HttpGet]
    [Route("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Quizz))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
    public async Task<IActionResult> GetQuizz([FromRoute]ulong id)
    {
        try
        {
            if(id < 1)
                return BadRequest(new { ErrorMessage = "ID is wrong."});

            var quizzResult = await _quizzService.GetByIdAsync(id);

            if(!quizzResult.IsSuccess || quizzResult.Data is null)
                return NotFound(new { ErrorMessage = quizzResult.ErrorMessage });

            return Ok(ToDto(quizzResult.Data));
        }
        catch(Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { ErrorMessage = e.Message });
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Quizz))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
    public async Task<IActionResult> PostQuizz(CreateQuizzDto model)
    {
        try
        {
            if(!ModelState.IsValid)
                return BadRequest(model);

            var createQuizzResult = await _quizzService.CreateAsync(model.Title!,model.PassWord!, model.Description!,model.StartTime, model.EndTime);
            if(!createQuizzResult.IsSuccess)
                return BadRequest(new { ErrorMessage = createQuizzResult.ErrorMessage });

            return CreatedAtAction(nameof(PostQuizz), new { Id = createQuizzResult?.Data?.Id }, ToDto(createQuizzResult?.Data!));
        }
        catch(Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { ErrorMessage = e.Message });
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
    public async Task<IActionResult> DeleteQuizz(ulong id)
    {
        try
        {
            var removeQuizzResult = await _quizzService.RemoveByIdAsync(id);

            if(!removeQuizzResult.IsSuccess || removeQuizzResult.Data is null)
                return NotFound(new { ErrorMessage = removeQuizzResult.ErrorMessage });

            return Ok();
        }
        catch(Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { ErrorMessage = e.Message });
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Quizz))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
    public async Task<IActionResult> UpdateQuizz([FromRoute]ulong id, [FromBody]UpdateQuizzDto model)
    {
        try
        {
            if(!ModelState.IsValid)
                return BadRequest(model);

            if(!await _quizzService.ExistsAsync(id))
                return NotFound(new { ErrorMessage = "Quizz with given ID not found." });

            var updateQuizzResult = await _quizzService.UpdateAsync(id, model.Title!,model.PassWord!, model.Description!,model.EndTime,model.StartTime);
            if(!updateQuizzResult.IsSuccess)
                return BadRequest(new { ErrorMessage = updateQuizzResult.ErrorMessage });

            return Ok(ToDto(updateQuizzResult?.Data!));
        }
        catch(Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { ErrorMessage = e.Message });
        }
    }
    
    private Quizz ToDto(Models.Quizz.Quizz entity)
    {
        return new Quizz()
        {
            Id = entity.Id,
            Title = entity.Title,
            PassWord = entity.PassWord,
            Description = entity.Description,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            EndTime = entity.EndTime,
            StartTime = entity.StartTime
        };
    }
}