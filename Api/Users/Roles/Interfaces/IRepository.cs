using Users.Roles.Entities;

namespace Users.Roles.Interfaces;

public interface IRepository
{
    void create(Dto dto);
    Model? read(Dto dto, bool track_changes = true);
    IEnumerable<Model> readAll(bool track_changes = true);
    void update(Dto dto);
    void delete(Dto dto);
    Task createAsync(Dto dto);
    Task<Model?> readAsync(Dto dto, bool track_changes = true);
    Task<IEnumerable<Model>> readAllAsync(bool track_changes = true);
    Task updateAsync(Dto dto);
    Task deleteAsync(Dto dto);
}
