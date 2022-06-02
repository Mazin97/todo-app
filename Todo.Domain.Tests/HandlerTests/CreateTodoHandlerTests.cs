using Todo.Domain.Commands;
using Todo.Domain.Handlers;
using Todo.Domain.Tests.Repositories;

namespace Todo.Domain.Tests.HandlerTests;

[TestClass]
public class CreateTodoHandlerTests
{
    private readonly CreateTodoCommand _invalidCommand = new CreateTodoCommand("", "", DateTime.Now);
    private readonly CreateTodoCommand _validCommand = new CreateTodoCommand("Salvar Gotham", "Bruce Wayne", DateTime.Now);
    private readonly TodoHandler _handler = new TodoHandler(new FakeTodoRepository());
    
    [TestMethod]
    public void Dado_um_comando_invalido_deve_interromper_a_execucao()
    {
        var result = (GenericCommandResult)_handler.Handle(_invalidCommand);
        Assert.IsFalse(result.Success);
    }
    
    [TestMethod]
    public void Dado_um_comando_valido_deve_criar_a_tarefa()
    {
        var result = (GenericCommandResult)_handler.Handle(_validCommand);
        Assert.IsTrue(result.Success);
    }
}