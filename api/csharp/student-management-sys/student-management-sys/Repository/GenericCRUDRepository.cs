using Microsoft.EntityFrameworkCore;
using student_management_sys.Configs;

namespace student_management_sys.Repository
{
    public class GenericCRUDRepository<T> : IGenericCRUDRepository<T> where T : class
    {
        protected readonly StudManSysDBContext context;
        internal DbSet<T> dbSet;
        public GenericCRUDRepository(StudManSysDBContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
            //this.logger = logger;
        }

        public virtual async Task<T> Insert(T entity)
        {
            var results = await dbSet.AddAsync(entity);
            return results.Entity;
        }
        public virtual async Task<T> FindById(string id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public virtual async Task Delete(string id)
        {
            var entity = await dbSet.FindAsync(id);
            context.Entry(entity).State = EntityState.Deleted;
        }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
