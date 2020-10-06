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
 * Description: Class for control all.
 */

namespace UMS.Controllers
{
    public class UnitOfWork : IUnitOfWork
    {
        private AuthDbContext _context;
        public ILogsRepository Logs { get; private set; }

        public UnitOfWork(AuthDbContext context)
        {
            _context = context;
            Logs = new LogsRepository(_context);
        } // End Constructor

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        } // End Dispose
    } // End UnitOfWork
}
