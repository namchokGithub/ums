/*
 * Name : EmailConfiguration.cs
 * Author: Namchok Singhachai
 * Description: Configuration for email service.
 */

namespace EmailService
{
    public class EmailConfiguration
    {
        public string From { set; get; } // Email sender
        public string SmtpServer { set; get; } // Smt server eg. stm.live.com, stm.gmail.com and stm.microsoft.com
        public int Port { set; get; } // Port eg. 465 or 24
        public string Username { set; get; } // Username for Email sender
        public string Password { set; get; } // Password for Email sender
    } // End EmainConfiguration 
} // End Email Service
