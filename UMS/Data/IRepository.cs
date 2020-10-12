/*
 * Name: IRepository
 * Author: Namchok Singchai
 * Description: For repository pattern.
 */

using System.Threading.Tasks;

namespace UMS.Data
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
