using FileConverter.DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using FileConverter.DataLayer.Model;

namespace FileConverter.DataLayer.Repositories;

public class AbstractRepository<TModel> : IAbstractRepository<TModel> where TModel : BaseModel
{
    protected FileConverterDbContext Context { get; }
    protected DbSet<TModel> DbSet { get; }

    public AbstractRepository(FileConverterDbContext context)
    {
        Context = context;
        DbSet = Context.Set<TModel>();
    }

    public virtual async Task CreateAsync(params TModel[] item)
    {
        await DbSet.AddRangeAsync(item);
        await Context.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(params TModel[] item)
    {
        foreach (var baseModel in item)
        {
            var entry = Context.Entry(baseModel);

            entry.State = EntityState.Modified;
        }

        await Context.SaveChangesAsync();
    }

    public virtual async Task<TModel?> FindByIdAsync(Guid id) =>
        await Include(DbSet.AsSplitQuery()).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<TModel?>> FindByIdAsync(IEnumerable<Guid> id) =>
        await Include(DbSet.AsSplitQuery()).AsNoTracking().Where(x => id.Contains(x.Id)).ToListAsync();

    public virtual async Task<IEnumerable<TModel>> GetAsync() =>
        await Include(DbSet.AsSplitQuery()).OrderBy(x => x.Id).AsNoTracking().ToArrayAsync();

    public async Task RemoveAsync(TModel item)
    {
        DbSet.Remove(item);
        await Context.SaveChangesAsync();
    }

    protected virtual IQueryable<TModel> Include(IQueryable<TModel> query) => query;

    public IQueryable<TModel> Filter(Expression<Func<TModel, bool>> predicate) =>
        Include(DbSet).AsSplitQuery().AsNoTracking().Where(predicate);
}