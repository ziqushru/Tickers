using Microsoft.EntityFrameworkCore;
using Repository;
using Users.Entities;
using Users.Interfaces;

namespace Users.Database;

public class Repository : BaseRepository<Model, Context>, IRepository
{
    public Repository(Context context)
        : base(context)
    {}

    public void create(Dto dto) =>
        this.insert(new Model()
        {
            google_id = dto.google_id,
            name = dto.name
        });

    public Model? read(Dto dto, bool track_changes) =>
        this.selectByCondition(model =>
                model.name.Equals(dto.name), track_changes)
            .SingleOrDefault();

    public IEnumerable<Model> readAll(bool track_changes) =>
        this.select(track_changes)
            .ToList();

    public void update(Dto dto)
    {
        var model = this.read(dto, false);
        if (model is not null)
        {
            this.update(model);
        }
    }

    public void delete(Dto dto)
    {
        var model = this.read(dto, false);
        if (model is null) return;
        this.delete(model);
    }

    public async Task createAsync(Dto dto) =>
        await this.insertAsync(new Model()
        {
            google_id = dto.google_id,
            name = dto.name
        });

    public async Task<Model?> readAsync(Dto dto, bool track_changes) =>
        await this.selectByCondition(model =>
                model.name.Equals(dto.name), track_changes)
            .SingleOrDefaultAsync();

    public async Task<IEnumerable<Model>> readAllAsync(bool track_changes) =>
        await this.select(track_changes)
            .ToListAsync();

    public async Task updateAsync(Dto dto)
    {
        var model = await this.readAsync(dto, false);
        if (model is null) return;
        await this.updateAsync(model);
    }

    public async Task deleteAsync(Dto dto)
    {
        var model = await this.readAsync(dto, false);
        if (model is null) return;
        await this.deleteAsync(model);
    }
}
