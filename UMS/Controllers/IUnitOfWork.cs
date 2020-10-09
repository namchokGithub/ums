using System;
using UMS.Data;

/*
 * Name: IUnitOfWork
 * Author: Namchok Singhachai
 * Description: The interface class for control all.
 */

namespace UMS.Controllers
{
    public interface IUnitOfWork : IDisposable
    {
        ILogsRepository Logs { get; }
        IAccountRepository Account { get; }
        int Commit();
        new void Dispose();
    } // End IUnitOfWork
}
