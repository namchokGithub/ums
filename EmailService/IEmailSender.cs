using System.Threading.Tasks;

/*
 * Name: IEmailSender
 * Author: Namchok Singhachai
 * Description: Interface for sending ab email.
 */

namespace EmailService
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
        Task SendEmailAsync(Message message);
    } // End Interface of class EmailSender
}
