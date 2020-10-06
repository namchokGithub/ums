using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UMS.Models;

/*
 * Name: LogsRepository
 * Author: Namchok Singhachai
 * Description: Logs to connect data layer.
 */

namespace UMS.Data
{
    public class LogsRepository : Repository<Logs>, ILogsRepository
    {
        protected readonly LogsContext _logContext;
        protected readonly ILogger<Logs> _logger;
        public LogsRepository(LogsContext context, ILogger<Logs> logger) : base(context)
        {
            _logger = logger;
        }

        /*
         * Name: GetAll
         * Parameter: numofrow(int)
         * Description: Get all logs top by numofrow(int)
         */
        public List<Logs> GetAll(int numofrow = 100)
        {
            _logger.LogDebug($"Getting top {numofrow} from all logs.");
            return _logContext.Logs.FromSqlRaw(@$"Exec dbo.ums_Get_all_log {numofrow}").ToList<Logs>();
        } // End GetAll
    } // End LogsRepository
}
