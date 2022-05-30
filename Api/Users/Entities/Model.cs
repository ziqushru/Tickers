using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Users.Entities;

public record Model : BaseModel
{
    public string google_id { get; init; }
    public string name { get; init; }
    public int t_Roles_id { get; set; }
    public Roles.Entities.Model role { get; init; }

    public static void OnModelCreating(ModelBuilder model_builder)
    {
        var entity_type_builder = model_builder.Entity<Model>()
            .ToTable("t_Users");

        entity_type_builder.HasKey(model => model.id);

        entity_type_builder.HasIndex(model => model.google_id, "i_google_id");
        entity_type_builder.HasIndex(model => model.t_Roles_id, "i_t_Roles_id");

        var index = 2;
        entity_type_builder.Property(model => model.google_id)
            .IsRequired()
            .HasMaxLength(100)
            .HasAnnotation("Order", index++);
        entity_type_builder.Property(model => model.name)
            .IsRequired()
            .HasMaxLength(100)
            .HasAnnotation("Order", index++);
        entity_type_builder.Property(model => model.t_Roles_id)
            .IsRequired()
            .HasAnnotation("Order", index);
    }
}
