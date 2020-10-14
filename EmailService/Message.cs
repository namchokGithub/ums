using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * Namspace: EmailService/Message.cs
 * Author: Namchok Singhachai
 * Description: Service for send email
 */

namespace EmailService
{
    public class Message
    {
        public List<MailboxAddress> To { set; get; } // An email for sending
        public string Subject { set; get; } // The subject of an email
        public string Content { set; get; } // The conten of an email
        public string htmlText { set; get; } // The html in an email (Optional)

        /*
         * Name: Message
         * Parametor: to(String), subject(string), content(String)
         * Author: Namchok Singhachai
         * Description: Constructor for set a messages.
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
