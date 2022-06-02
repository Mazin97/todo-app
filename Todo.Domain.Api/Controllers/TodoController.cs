using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Commands;
using Todo.Domain.Entites;
using Todo.Domain.Handlers;
using Todo.Domain.Repositories;

namespace Todo.Domain.Api.Controllers;

[ApiController]
[Authorize]
[Route("v1/todos")]
public class TodoController : ControllerBase
{
    [HttpGet("")]
    public IEnumerable<TodoItem> Get(
        [FromServices]ITodoRepository repository
    )
    {
        var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        return repository.GetAll(user);
    }
    
    [HttpGet("done")]
    public IEnumerable<TodoItem> GetDone(
        [FromServices]ITodoRepository repository
    )
    {
        var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        return repository.GetAllDone(user);
    }
    
    [HttpGet("undone")]
    public IEnumerable<TodoItem> GetUndone(
        [FromServices]ITodoRepository repository
    )
    {
        var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        return repository.GetAllUndone(user);
    }
    
    [HttpGet("by-period")]
    public IEnumerable<TodoItem> GetUndone(
        [FromQuery] DateTime period,
        [FromQuery] bool done,
        [FromServices]ITodoRepository repository
    )
    {
        var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        return repository.GetAllByPeriod(user, period, done);
    }

    [HttpPost("")]
    public GenericCommandResult Create(
        [FromBody] CreateTodoCommand command,
        [FromServices] TodoHandler handler
        )
    {
        command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        return (GenericCommandResult)handler.Handle(command);
    }
    
    [HttpPut()]
    public GenericCommandResult Update(
        [FromBody] UpdateTodoCommand command,
        [FromServices] TodoHandler handler
    )
    {
        command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        return (GenericCommandResult)handler.Handle(command);
    }
    
    [HttpPut("mark-as-done")]
    public GenericCommandResult MarkAsDone(
        [FromBody] MarkTodoAsDoneCommand command,
        [FromServices] TodoHandler handler
    )
    {
        command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        return (GenericCommandResult)handler.Handle(command);
    }
    
    [HttpPut("mark-as-undone")]
    public GenericCommandResult MarkAsUndone(
        [FromBody] MarkTodoAsUndoneCommand command,
        [FromServices] TodoHandler handler
    )
    {
        command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        return (GenericCommandResult)handler.Handle(command);
    }
}