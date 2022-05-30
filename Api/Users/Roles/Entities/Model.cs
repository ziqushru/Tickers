using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Users.Roles.Entities;

public record Model : BaseModel
{
    public string description { get; init; }
    public IList<Permissions.Entities.Model> permissions { get; init; }
    public IList<RolePermissions.Entities.Model> role_permissions { get; init; }

    public static void OnModelCreating(ModelBuilder model_builder)
    {
        var entity_type_builder = model_builder.Entity<Model>()
            .ConfigureBaseEntity()
            .ToTable("t_Roles");

        entity_type_builder.HasKey(role => role.id);

        entity_type_builder.HasIndex(role => role.description, "i_description");

        var index = 2;
        entity_type_builder.Property(role => role.description)
            .IsRequired()
            .HasMaxLength(25)
            .HasAnnotation("Order", index);
    }
}
