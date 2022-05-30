using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Users.Permissions.Entities;

public record Model : BaseModel
{
    public string description { get; init; }
    public ICollection<Roles.Entities.Model> roles { get; init; }
    public ICollection<RolePermissions.Entities.Model> role_permissions { get; init; }

    public static void OnModelCreating(ModelBuilder model_builder)
    {
        var entity_type_builder = model_builder.Entity<Model>()
            .ConfigureBaseEntity()
            .ToTable("t_Permissions");

        entity_type_builder.HasKey(permission => permission.id);

        entity_type_builder.HasIndex(permission => permission.description, "i_description");

        var index = 2;
        entity_type_builder.Property(permission => permission.description)
            .IsRequired()
            .HasMaxLength(25)
            .HasAnnotation("Order", index);
    }
}
