using UMS.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

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
        Task<List<Logs>> GetAllAsync(int numofrow);
        List<Logs> Search(string messageInput, string dateInput);
        Task<List<Logs>> SearchAsync(string messageInput, string dateInput);
    } // End ILogsRepository
}
