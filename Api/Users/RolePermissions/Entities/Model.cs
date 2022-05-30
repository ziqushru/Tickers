using Microsoft.EntityFrameworkCore;

namespace Users.RolePermissions.Entities;

public record Model
{
    public int t_Roles_id { get; init; }
    public Roles.Entities.Model role { get; init; }
    public int t_Permissions_id { get; init; }
    public Permissions.Entities.Model permission { get; init; }

    public static void OnModelCreating(ModelBuilder model_builder)
    {
        var entity_type_builder = model_builder.Entity<Roles.Entities.Model>()
            .HasMany(role => role.permissions)
            .WithMany(permission => permission.roles)
            .UsingEntity<Model>(
                r =>
                    r.HasOne(role_permission => role_permission.permission)
                        .WithMany(permission => permission.role_permissions)
                        .HasForeignKey(role_permission => role_permission.t_Permissions_id),
                l =>
                    l.HasOne(role_permission => role_permission.role)
                        .WithMany(role => role.role_permissions)
                        .HasForeignKey(role_permission => role_permission.t_Roles_id))
            .ToTable("t_UserPermissions");

        entity_type_builder.HasKey(role_permission =>
            new
            {
                role_permission.t_Roles_id,
                role_permission.t_Permissions_id
            });

        entity_type_builder.HasIndex(role_permission => role_permission.t_Roles_id, "i_Roles_id");
        entity_type_builder.HasIndex(role_permission => role_permission.t_Permissions_id, "i_Permissions_id");
    }
}
