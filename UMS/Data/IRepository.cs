using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
 * Name: IRepository
 * Author: Namchok Singchai
 * Description: For repository pattern.
 */

namespace UMS.Data
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(string id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(string id);
    } // End interface IRepository
}
