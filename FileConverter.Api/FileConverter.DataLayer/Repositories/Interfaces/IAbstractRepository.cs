using FileConverter.DataLayer.Model;
using System.Linq.Expressions;

namespace FileConverter.DataLayer.Repositories.Interfaces;

public interface IAbstractRepository<TModel> where TModel : BaseModel
{
    Task CreateAsync(params TModel[] item);
    Task UpdateAsync(params TModel[] item);
    Task<TModel?> FindByIdAsync(Guid id);
    Task<IEnumerable<TModel?>> FindByIdAsync(IEnumerable<Guid> id);
    Task<IEnumerable<TModel>> GetAsync();
    IQueryable<TModel> Filter(Expression<Func<TModel, bool>> predicate);
    Task RemoveAsync(TModel item);
}
