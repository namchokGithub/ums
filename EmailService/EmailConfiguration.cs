using System;

namespace EmailService
{
    public class EmailConfiguration
    {
        public string From { set; get; }
        public string SmtpServer { set; get; }
        public int Port { set; get; }
        public string Username { set; get; }
        public string Password { set; get; }
    }
}
