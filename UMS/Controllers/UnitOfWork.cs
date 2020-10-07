using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UMS.Data;
using System.Security.Claims;
using UMS.Models;
using Microsoft.AspNetCore.Mvc;

/*
 * Name: IUnitOfWork
 * Author: Namchok Singhachai
 * Description: The class for control all.
 */

namespace UMS.Controllers
{
    public class UnitOfWork : IUnitOfWork
    {
        private AuthDbContext _context;
        public ILogsRepository Logs { get; private set; }
        /*
         * Name: UnitOfWork
         * Parameter: context(AuthDbContext)
         * Author: Namchok Singhachai
         * Description: The constructor for manage all query.
         */
        public UnitOfWork(AuthDbContext context)
        {
            _context = context;
            Logs = new LogsRepository(_context);
        } // End Constructor

        /*
         * Name: Commit
         * Author: Namchok Singhachai
         * Description: Saving changes.
         */
        public int Commit()
        {
            return _context.SaveChanges();
        } // End commit

        /*
         * Name: Dispose
         * Author: Namchok Singhachai
         * Description: Discardation all operation.
         */
        public void Dispose()
        {
            _context.Dispose();
        } // End Dispose
    } // End UnitOfWork
}
