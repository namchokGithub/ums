using System;
using UMS.Data;
using System.Threading.Tasks;

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
        Task<int> CommitAsync();
        new void Dispose();
        Task DisposeAsync();
    } // End IUnitOfWork
}
