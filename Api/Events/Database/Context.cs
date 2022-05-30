using Events.Entities;
using Microsoft.EntityFrameworkCore;

namespace Events.Database;

public class Context : DbContext
{
    public DbSet<Model> events { get; set; }
   
    public Context()
    {
    }

    public Context(DbContextOptions<Context> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder model_builder)
    {
        model_builder.HasAnnotation("Relational:Collation", "Greek_CI_AS");
    
        Entities.Model.OnModelCreating(model_builder);
    
        base.OnModelCreating(model_builder);
    }
}
