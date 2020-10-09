using UMS.Data;
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