using Todo.Domain.Entites;

namespace Todo.Domain.Tests.EntityTests;

[TestClass]
public class TodoItemTests
{
    private readonly TodoItem _todo = new TodoItem("Fazer compras", DateTime.Now, "Mateus");
    
    [TestMethod]
    public void Dado_um_novo_todo_o_mesmo_nao_pode_ser_concluido()
    {
        Assert.IsFalse(_todo.Done);
    }
}