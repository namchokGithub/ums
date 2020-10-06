/*
 * Name: IRepository
 * Author: Namchok Singchai
 * Description: For repository pattern.
 */

namespace UMS.Data
{
    public interface IRepository<T> where T : class
    {
        T Get(string id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Dispose();
        int Complete();
    } // End interface IRepository
}
