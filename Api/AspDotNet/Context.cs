using Microsoft.EntityFrameworkCore;
using Users.Permissions.Entities;

namespace AspDotNet;

public class Context : DbContext
{
    public DbSet<Model>? permissions { get; set; }
    public DbSet<Users.Roles.Entities.Model>? roles { get; set; }
    public DbSet<Users.Entities.Model>? users { get; set; }
    public DbSet<Events.Entities.Model>? events { get; set; }

    public Context(DbContextOptions<Context> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder model_builder)
    {
        model_builder.HasAnnotation("Relational:Collation", "Greek_CI_AS");
        
        Users.Permissions.Entities.Model.OnModelCreating(model_builder);
        Users.Roles.Entities.Model.OnModelCreating(model_builder);
        Users.RolePermissions.Entities.Model.OnModelCreating(model_builder);
        Users.Entities.Model.OnModelCreating(model_builder);
        Events.Entities.Model.OnModelCreating(model_builder);
        
        base.OnModelCreating(model_builder);
    }
}
