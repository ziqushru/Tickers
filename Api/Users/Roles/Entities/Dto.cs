namespace Users.Roles.Entities;

public record Dto
{
    public string description { get; init; }
    public IList<Permissions.Entities.Dto> permissions { get; init; }
}
