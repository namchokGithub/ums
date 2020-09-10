using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using MimeKit;
using NETCore.MailKit.Core;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

/*
 * Name : EmailSender (extend: IEmailSender)
 * Author: Namchok Singhachai
 * Description: Configuration of Email service
 */

namespace EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig; // Email configuration
        private readonly ILogger<EmailSender> _logger; // Email configuration

        /*
         * Name: EmailSender(Constructor)
         * Parameter: emailConfig(EmailConfiguration)
         * Author: Namchok Singhachai
         * Description: Set email config
         */
        public EmailSender(EmailConfiguration emailConfig, ILogger<EmailSender> logger)
        {
            _emailConfig = emailConfig; // Set config
            _logger = logger;
            _logger.LogDebug("Email sender.");
        } // End EmailConfig

        /*
         * Name: SendEmail
         * Parameter: message(Message)
         * Author: Namchok Singhachai
         * Description: Send the email
         */
        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message); // Create email messages
            _logger.LogTrace("Email Sender: Sending email.");
            Send(emailMessage);
        } // End SendEmail

        /*
         * Name: CreateEmailMessage
         * Parameter: message(Message)
         * Author: Namchok Singhachai
         * Description: Create the email messages
         */
        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();                           // New messages form
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From));   // Set sender 
            emailMessage.To.AddRange(message.To);                           // Set reciver 
            emailMessage.Subject = message.Subject;                         // Set subject of email
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Content }; // Set content
            _logger.LogTrace("Creating message.");
            return emailMessage;
        } // End CreateEmailMessage

        /*
         * Name: Send
         * Parameter: mailMessage(MimeMessage)
         * Author: Namchok Singhachai
         * Description: Send the email
         */
        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    _logger.LogTrace("Email Sender: Start Sending email.");
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);   // Connect email sender
                    client.AuthenticationMechanisms.Remove("XOAUTH2");                  // Remove authenication (OAUTH 2)
                    client.Authenticate(_emailConfig.Username, _emailConfig.Password);  // Authen user 

                    client.Send(mailMessage); // Send email
                    _logger.LogTrace("Email Sender: End Sending email.");
                }
                catch
                {
                    _logger.LogTrace("Email Sender: End Sending email.");
                    throw;
                }
                finally
                {
                    client.Disconnect(true); // Disconnet email
                    client.Dispose();
                    _logger.LogTrace("Email Sender: End Sending email.");
                } // Edn try catch
            }
        }// End Send

        /*
         * Name: SendEmailAsync
         * Parameter: message(Message)
         * Author: Namchok Singhachai
         * Description: Create massage and send email
         */
        public async Task SendEmailAsync(Message message)
        {
            var mailMessage = CreateEmailMessage(message);
            _logger.LogTrace("Email Sender: Sending email (Async).");
            await SendAsync(mailMessage);
        } // End SendEmailAsync

        /*
         * Name: SendAsync
         * Parameter: mailMessage(MimeMessage)
         * Author: Namchok Singhachai
         * Description: Send the email
         */
        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    _logger.LogTrace("Email Sender: Start Sending email (Async).");
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.Username, _emailConfig.Password);

                    await client.SendAsync(mailMessage);
                    _logger.LogTrace("Email Sender: End Sending email (Async).");
                }
                catch
                {
                    _logger.LogTrace("Email Sender: End Sending email (Async).");
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                    _logger.LogTrace("Email Sender: End Sending email (Async).");
                } // End try catch
            }
        } // End SendAsync
    } // End Class EmailSender
}
