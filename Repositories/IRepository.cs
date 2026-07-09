public interface IRepository<TEntity>{
    public abstract Task<TEntity?> GetByIdAsync(Guid id);

    public abstract Task<List<TEntity>> GetAllAsync();

    public abstract  Task<TEntity> AddAsync(TEntity entity);

    public abstract  Task<TEntity> UpdateAsync(TEntity entity);

    public abstract Task DeleteAsync(TEntity entity);
}