using MimeKit;
using System.Linq;
using System.Collections.Generic;

/*
 * Name: Message.cs
 * Author: Namchok Singhachai
 * Description: Message for sending an email.
 */

namespace EmailService
{
    public class Message
    {
        public List<MailboxAddress> To { set; get; } // An email for sending
        public string Subject { set; get; } // The subject of an email
        public string Content { set; get; } // The conten of an email
        public string HtmlText { set; get; } // The html in an email (Optional)
        /*
         * Name: Message
         * Parametor: to(IEnumerable<string>), subject(string), content(String)
         * Author: Namchok Singhachai
         * Description: Setting a messages.
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
