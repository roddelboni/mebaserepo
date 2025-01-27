using Base.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Base.Data.Repositories;

public abstract class Repository<TEntity > where TEntity : class
{
    private readonly BaseContext _context;
    protected Repository(BaseContext context) => _context = context;

    public async Task Create(TEntity entity, CancellationToken cancellationToken=default)
        => await _context.Set<TEntity>().AddAsync(entity, cancellationToken);

    public async Task Create(IEnumerable<TEntity> entities, CancellationToken cancellationToken=default)
        => await _context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);

    public void Update(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

}
