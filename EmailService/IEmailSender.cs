using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

/*
 * Name: IEmailSender
 * Namspace: EmailService
 * Author: Namchok Singhachai
 * Description: Interface for send email.
 */

namespace EmailService
{
    public interface IEmailSender
    {
        void SendEmail(Message message);

        Task SendEmailAsync(Message message);
    } // End Interface of EmailSender
}
