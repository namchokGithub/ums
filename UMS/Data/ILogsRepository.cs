using System.Threading.Tasks;
using System.Collections.Generic;
using User_Management_System.Models;

/*
 * Name: ILogsRepository
 * Author: Namchok Singhachai
 * Description: Interface for logs to connect data layer.
 */

namespace User_Management_System.Data
{
    public interface ILogsRepository : IRepository<Logs>
    {
        List<Logs> GetAll(int numofrow);
        Task<List<Logs>> GetAllAsync(int numofrow);
        List<Logs> Search(string messageInput, string dateInput);
        Task<List<Logs>> SearchAsync(string messageInput, string dateInput);
    } // End ILogsRepository
}