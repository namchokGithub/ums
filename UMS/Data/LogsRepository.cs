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
 * Description: Class of logs to connect data layer.
 */

namespace UMS.Data
{
    public class LogsRepository : Repository<Logs>, ILogsRepository
    {
        protected readonly AuthDbContext _context;
        /*
         * Name: LogsRepository
         * Parametor: context(AuthDbContext)
         * Description: The constructor for set context
         */
        public LogsRepository(AuthDbContext context) : base(context)
        {
            _context = context;
        } // End constructor

        /*
         * Name: ~LogsRepository
         * Parametor: context(AuthDbContext)
         * Description: Deconstructor
         */
        ~LogsRepository()
        {
            _context.Dispose();
        } // End Deconstructor

        /*
         * Name: GetAll
         * Parameter: numofrow(int)
         * Description: Get all logs top by numofrow(int)
         */
        public List<Logs> GetAll(int numofrow)
        {
            return _context.Logs.FromSqlRaw(@$"Exec dbo.ums_Get_all_log {numofrow}").ToList();
        } // End GetAll

        /*
         * Name: Search
         * Parameter: messageInput(string), dateInput(string)
         * Description: Search log by message or date
         */
        public List<Logs> Search(string messageInput, string dateInput)
        {
            string sqlGetLog;
            if (dateInput != null && dateInput != "")
            {
                DateTime dateInputStart = Convert.ToDateTime(dateInput.Substring(0, (dateInput.IndexOf("-"))).ToString());
                DateTime dateInputEnd = Convert.ToDateTime(dateInput.Substring((dateInput.IndexOf("-")) + 1).ToString()); // Set date for query
                if (messageInput != "")
                    sqlGetLog = @$"Exec dbo.ums_Search_log '{dateInputStart}', '{dateInputEnd}', '{messageInput}'";
                else
                    sqlGetLog = @$"Exec dbo.ums_Search_log '{dateInputStart}', '{dateInputEnd}', ''";
            }
            else
            {
                sqlGetLog = @$"Exec dbo.ums_Search_log '', '', '{messageInput}'";
            } // End if date input not null
            return _context.Logs.FromSqlRaw(sqlGetLog).ToList() ?? throw new Exception("Calling a method on a null object reference.");
        } // End Search
    } // End LogsRepository
}