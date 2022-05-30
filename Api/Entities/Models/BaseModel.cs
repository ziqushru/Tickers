using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Models;

public record BaseModel
{
    public int id { get; init; }
    public bool is_deleted { get; set; }
    public DateTime create_date { get; init; }
    public DateTime update_date { get; set; }
    public DateTime? delete_date { get; set; }
}

public static class BaseEntityConfiguration
{
    public static EntityTypeBuilder<TEntity> ConfigureBaseEntity<TEntity>(this EntityTypeBuilder<TEntity> entity_type_builder) where TEntity : class
    {
        entity_type_builder.Property("id")
            .IsRequired()
            .HasColumnName("id")
            .HasAnnotation("Order", 1);
        entity_type_builder.Property("is_deleted")
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnName("is_deleted");
        entity_type_builder.Property("create_date")
            .IsRequired()
            .HasColumnName("create_date");
        entity_type_builder.Property("update_date")
            .IsRequired()
            .HasColumnName("update_date");
        entity_type_builder.Property("delete_date")
            .IsRequired(false)
            .HasColumnName("delete_date");
        
        return entity_type_builder;
    }
}