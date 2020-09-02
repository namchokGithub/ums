using System;

/*
 * Name : EmailConfiguration
 * Author: Namchok
 * Description: Configuration of Email service
 */

namespace EmailService
{
    public class EmailConfiguration
    {
        public string From { set; get; } // Sender
        public string SmtpServer { set; get; } // Smt server eg. stm.live.com, stm.gmail.com and stm.microsoft.com
        public int Port { set; get; } // Port eg. 465 or 24
        public string Username { set; get; } // Username of Email' sender
        public string Password { set; get; } // Password of Email' sender
    } // End EmainConfiguration 
}
