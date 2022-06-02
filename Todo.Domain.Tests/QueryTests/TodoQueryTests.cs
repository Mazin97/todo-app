using Todo.Domain.Entites;
using Todo.Domain.Queries;

namespace Todo.Domain.Tests.QueryTests;

[TestClass]
public class TodoQueryTests
{
    private List<TodoItem> _items;

    public TodoQueryTests()
    {
        _items = new List<TodoItem>()
        {
            new TodoItem("Salvar Gotham", DateTime.Now, "Bruce Wayne"),
            new TodoItem("Tarefa 1", DateTime.Now, "Bruce Wayne"),
            new TodoItem("Tarefa 2", DateTime.Now, "Bruce Wayne"),
            new TodoItem("Tarefa 3", DateTime.Now, "Robin Hood"),
            new TodoItem("Tarefa 4", DateTime.Now, "Robin Hood"),
        };
    }

    [TestMethod]
    public void Deve_retornar_tarefas_apenas_do_usuario_brucewayne()
    {
        var result = _items
            .AsQueryable()
            .Where(TodoQueries.GetAll("Bruce Wayne"))
            .ToList();
        
        Assert.AreEqual(3, result.Count);
    }
    
    [TestMethod]
    public void Deve_retornar_tarefas_prontas_do_usuario_brucewayne()
    {
        var randomTask = _items.First();
        randomTask.MarkAsDone();
        
        var result = _items
            .AsQueryable()
            .Where(TodoQueries.GetAllDone("Bruce Wayne"))
            .ToList();
        
        randomTask.MarkAsUndone();
        
        Assert.AreEqual(1, result.Count);
    }
    
    [TestMethod]
    public void Deve_retornar_tarefas_nao_prontas_do_usuario_brucewayne()
    {
        var randomTask = _items.First();
        randomTask.MarkAsDone();
        
        var result = _items
            .AsQueryable()
            .Where(TodoQueries.GetAllUndone("Bruce Wayne"))
            .ToList();
        
        randomTask.MarkAsUndone();
        
        Assert.AreEqual(2, result.Count);
    }

    [TestMethod]
    public void Deve_retornar_tasks_por_dia_do_usuario_brucewayne()
    {
        var result = _items
            .AsQueryable()
            .Where(TodoQueries.GetByPeriod("Bruce Wayne", DateTime.Now, false))
            .ToList();
        
        Assert.AreEqual(3, result.Count);
    }
}