using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UMS.Models;

/*
 * Name: ILogsRepository
 * Author: Namchok Singhachai
 * Description: Interface for logs to connect data layer.
 */

namespace UMS.Data
{
    public interface ILogsRepository : IRepository<Logs>
    {
        List<Logs> GetAll(int numofrow);
    } // End ILogsRepository
}
