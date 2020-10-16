using System;
using System.Threading.Tasks;
using User_Management_System.Data;

/*
 * Name: IUnitOfWork
 * Author: Namchok Singhachai
 * Description: The interface class for control all.
 */

namespace User_Management_System.Controllers
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