using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
 * Name: Repository
 * Author: Namchok Singhachai
 * Description: For repository parttern.
 */

namespace UMS.Data
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
    } // End Repository
}
