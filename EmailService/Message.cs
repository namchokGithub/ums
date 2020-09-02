using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * Namspace: EmailService
 * Author: Namchok Singhachai
 * Description: Service for send email
 */

namespace EmailService
{
    public class Message
    {
        public List<MailboxAddress> To { set; get; } // Email to send
        public string Subject { set; get; } // Subject of email
        public string Content { set; get; } // Conten of email
        public string htmlText { set; get; } // Html in email (Optional)

        /*
         * Name: Message (Constructor)
         * Parametor: to(String), subject(string), content(String)
         * Author: Namchok Singhachai
         * Description: Set messages
         */
        public Message(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress(x)));
            Subject = subject;
            Content = content;
        } // End Message
    } // End Message Class
}
