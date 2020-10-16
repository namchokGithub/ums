using System.Threading.Tasks;

/*
 * Name: IRepository
 * Author: Namchok Singchai
 * Description: The interface for repository pattern.
 */

namespace User_Management_System.Data
{
    public interface IRepository<T> where T : class
    {
        T Get(string id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Dispose();
        Task DisposeAsync();
        int Complete();
        Task<int> CompleteAsync();
    } // End interface IRepository
}
