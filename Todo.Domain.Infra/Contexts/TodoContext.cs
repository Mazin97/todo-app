using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entites;

namespace Todo.Domain.Infra.Contexts;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoItem>().Property(x => x.Id);
        modelBuilder.Entity<TodoItem>().Property(x => x.User).HasMaxLength(120).HasColumnType("varchar");
        modelBuilder.Entity<TodoItem>().Property(x => x.Title).HasMaxLength(160).HasColumnType("varchar");
        modelBuilder.Entity<TodoItem>().Property(x => x.Done).HasColumnType("bit");
        modelBuilder.Entity<TodoItem>().Property(x => x.Date).HasColumnType("smalldatetime");
        modelBuilder.Entity<TodoItem>().HasIndex(b => b.User);
    }
    
    public DbSet<TodoItem> Todos { get; set; }
}