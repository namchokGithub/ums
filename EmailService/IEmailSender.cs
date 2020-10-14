using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

/*
 * Name: IEmailSender
 * Namspace: EmailService
 * Author: Namchok Singhachai
 * Description: Interface for send email
 */

namespace EmailService
{
    public interface IEmailSender
    {
        /*
         * Name: SendEmail
         * Parameter: message(Message)
         * Author: Namchok Singhachai
         * Description: Send email with your messages
         */
        void SendEmail(Message message); // End SendEmail

        /*
         * Name: SendEmailAsync
         * Parameter: message(Message)
         * Author: Namchok Singhachai
         * Description: Send email with your messages (Async)
         */
        Task SendEmailAsync(Message message); // End SendEmailAsync
    } // End IEmailSender
}