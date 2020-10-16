using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

/*
 * Name: Repository
 * Author: Namchok Singhachai
 * Description: For repository parttern.
 */

namespace User_Management_System.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        public Repository(DbContext context)
        {
            Context = context;
        } // End constructor

        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        } // Add entity

        public void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        } // Delete entity

        public T Get(string id)
        {
            return Context.Set<T>().Find(id);
        } // Get entity

        public void Update(T entity)
        {
            Context.Update<T>(entity);
        } // Update entity

        public int Complete()
        {
            return Context.SaveChanges();
        } // End commit

        public async Task<int> CompleteAsync()
        {
            return await Context.SaveChangesAsync();
        } // End CompleteAsync

        public void Dispose()
        {
            Context.Dispose();
        } // End dispose

        public async Task DisposeAsync()
        {
            await Context.DisposeAsync();
        } // End DisposeAsync
    } // End Repository
}