using System;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

/*
 * Name : EmailSender (extend: IEmailSender)
 * Author: Namchok Singhachai
 * Description: Configuration of email service.
 */

namespace EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;
        private readonly EmailConfiguration _emailConfig;
        /*
         * Name: EmailSender
         * Parameter: emailConfig(EmailConfiguration)
         * Author: Namchok Singhachai
         * Description: Setting a messages.
         */
        public EmailSender(EmailConfiguration emailConfig, ILogger<EmailSender> logger)
        {
            _emailConfig = emailConfig;
            _logger = logger;
            _logger.LogTrace("Start email sender.");
        } // End email sender constructor

        /*
         * Name: SendEmail
         * Parameter: message(Message)
         * Author: Namchok Singhachai
         * Description: Sending an email.
         */
        public void SendEmail(Message message)
        {
            try
            {
                _logger.LogTrace("Start send an email.");
                var emailMessage = CreateEmailMessage(message); // Create email messages
                _logger.LogTrace("Email Sender: Sending an email.");
                Send(emailMessage);
            }
            catch (Exception e)
            {
                _logger.LogTrace("End send an email.");
                throw e;
            } // End try catch
        } // End SendEmail

        /*
         * Name: CreateEmailMessage
         * Parameter: message(Message)
         * Author: Namchok Singhachai
         * Description: Creating an email messages and return message.
         */
        private MimeMessage CreateEmailMessage(Message message)
        {
            try
            {
                _logger.LogTrace("Start creating an email message.");
                _logger.LogDebug("Setting an email message.");
                var emailMessage = new MimeMessage();                           // New messages
                emailMessage.From.Add(new MailboxAddress(_emailConfig.From));   // Setting sender 
                emailMessage.To.AddRange(message.To);                           // Setting reciver 
                emailMessage.Subject = message.Subject;                         // Setting subject 
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Content }; // Setting content
                _logger.LogDebug("Creating a message.");
                _logger.LogTrace("End create an email message.");
                return emailMessage;
            }
            catch (Exception e)
            {
                _logger.LogTrace("End create an email message.");
                throw e;
            } // End try catch
        } // End CreateEmailMessage

        /*
         * Name: Send
         * Parameter: mailMessage(MimeMessage)
         * Author: Namchok Singhachai
         * Description: Sending an email.
         */
        private void Send(MimeMessage mailMessage)
        {
            try
            {
                using var client = new SmtpClient();
                try
                {
                    _logger.LogTrace("Email Sender: Start sending an email.");
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);   // Connect email sender
                    client.AuthenticationMechanisms.Remove("XOAUTH2");                  // Remove authenication (OAUTH 2)
                    client.Authenticate(_emailConfig.Username, _emailConfig.Password);  // Authen an user 
                    client.Send(mailMessage); // Send an email
                    _logger.LogInformation("Sending an email.");
                    _logger.LogTrace("Email Sender: End sending an email.");
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    client.Disconnect(true); // Disconnet email
                    client.Dispose();
                    _logger.LogTrace("=End Sending an email.");
                } // End try catch
            }
            catch (Exception e)
            {
                _logger.LogTrace("End sending an email.");
                throw e;
            } // Edn try catch
        }// End Send

        /*
         * Name: SendEmailAsync
         * Parameter: message(Message)
         * Author: Namchok Singhachai
         * Description: Creating a message and sending an email.
         */
        public async Task SendEmailAsync(Message message)
        {
            try
            {
                var mailMessage = CreateEmailMessage(message);
                _logger.LogTrace("Email Sender: Sending an email (Async).");
                await SendAsync(mailMessage);
            }
            catch (Exception e)
            {
                _logger.LogTrace("End send an email (Async).");
                throw e;
            }
        } // End SendEmailAsync

        /*
         * Name: SendAsync
         * Parameter: mailMessage(MimeMessage)
         * Author: Namchok Singhachai
         * Description: Sending an email.
         */
        private async Task SendAsync(MimeMessage mailMessage)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    try
                    {
                        _logger.LogTrace("Email Sender: Start Sending an email (Async).");
                        await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                        client.AuthenticationMechanisms.Remove("XOAUTH2");
                        await client.AuthenticateAsync(_emailConfig.Username, _emailConfig.Password);
                        await client.SendAsync(mailMessage);
                        _logger.LogInformation("Sending an email.");
                        _logger.LogTrace("Email Sender: End sending an email (Async).");
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    finally
                    {
                        await client.DisconnectAsync(true);
                        client.Dispose();
                        _logger.LogTrace("End sending an email (Async).");
                    } // End try catch
                }
            }
            catch (Exception e)
            {
                _logger.LogTrace("End Send an email (Async).");
                throw e;
            } // End try catch
        } // End SendAsync
    } // End Class EmailSender
}