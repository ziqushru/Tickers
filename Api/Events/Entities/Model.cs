using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Events.Entities;

public record Model : BaseModel
{
    public string name { get; init; }
    public string place { get; init; }

    public static void OnModelCreating(ModelBuilder model_builder)
    {
        var entity_type_builder = model_builder.Entity<Model>()
            .ToTable("t_Events");

        entity_type_builder.HasKey(model => model.id);

        entity_type_builder.HasIndex(model => model.name, "i_name");
        entity_type_builder.HasIndex(model => model.place, "i_place");

        var index = 2;
        entity_type_builder.Property(model => model.name)
            .IsRequired()
            .HasMaxLength(100)
            .HasAnnotation("Order", index++);
        entity_type_builder.Property(model => model.place)
            .IsRequired()
            .HasMaxLength(100)
            .HasAnnotation("Order", index);
    }
}
