namespace AssetChange.Service.Services.Interfaces
{
    public interface IService<T> where T : class
    {        
        public Task AddAsync(T entity);
        public Task RemoveAsync(T entity);
        public Task RefreshAsync(T entity);
        public Task<T> GetAsync(T entity);
        public Task<IEnumerable<T>> GetMoreAsync(T entity);
    }
}
