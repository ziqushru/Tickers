using Users.Entities;

namespace Users.Interfaces;

public interface IJwtService
{
    public string generate(Dto dto);
    public string? check(string jwt);
}
