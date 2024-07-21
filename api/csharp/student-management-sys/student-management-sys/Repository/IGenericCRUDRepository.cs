namespace student_management_sys.Repository
{
    public interface IGenericCRUDRepository<T> where T : class
    {
        Task<T> Insert(T entity);
        Task<T> FindById(string id);
        Task<IEnumerable<T>> GetAll();
        Task Update(T entity);
        Task Delete(string id);
        Task CompleteAsync();
    }
}
