namespace AssetChange.Infra.Data.Repositories.Interfaces
{
    public interface IRepository<T>  where T : class
    {
        public Task CreateAsync(T entity);
        public Task DeleteAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task<T> DetailsAsync (T entity);
        public Task<IEnumerable<T>> RetrieveAsync(T entity);
    }
}
