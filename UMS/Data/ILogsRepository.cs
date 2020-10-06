using System.Collections.Generic;
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
        List<Logs> Search(string messageInput, string dateInput);
    } // End ILogsRepository
}
