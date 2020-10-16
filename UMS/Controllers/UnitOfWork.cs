using System.Threading.Tasks;
using User_Management_System.Data;
/*
 * Name: IUnitOfWork
 * Author: Namchok Singhachai
 * Description: The class for control all.
 */

namespace User_Management_System.Controllers
{
    public class UnitOfWork : IUnitOfWork
    {
        private AuthDbContext _context;
        public ILogsRepository Logs { get; private set; }
        public IAccountRepository Account { get; private set; }
        /*
         * Name: UnitOfWork
         * Parameter: context(AuthDbContext)
         * Author: Namchok Singhachai
         */
        public UnitOfWork(AuthDbContext context)
        {
            _context = context;
            Logs = new LogsRepository(_context);
            Account = new AccountRepository(_context);
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
         * Name: CommitAsync
         * Author: Namchok Singhachai
         * Description: Saving changes.
         */
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        } // End CommitAsync

        /*
         * Name: Dispose
         * Author: Namchok Singhachai
         * Description: Discardation all operation.
         */
        public void Dispose()
        {
            _context.Dispose();
        } // End Dispose

        /*
         * Name: DisposeAsync
         * Author: Namchok Singhachai
         * Description: Discardation all operation.
         */
        public async Task DisposeAsync()
        {
            await _context.DisposeAsync();
        } // End DisposeAsync
    } // End UnitOfWork
}