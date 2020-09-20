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
            try 
            {
                _emailConfig = emailConfig; // Set config
                _logger = logger;
                _logger.LogTrace("Start Email sender.");
            } catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                _logger.LogTrace("End Email sender constructor.");
            } // End try catch
        } // End EmailConfig

        /*
         * Name: SendEmail
         * Parameter: message(Message)
         * Author: Namchok Singhachai
         * Description: Send the email
         */
        public void SendEmail(Message message)
        {
            try
            {
                _logger.LogTrace("Start Send Email.");
                var emailMessage = CreateEmailMessage(message); // Create email messages
                _logger.LogTrace("Email Sender: Sending email.");
                Send(emailMessage);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                _logger.LogTrace("End Email sender constructor.");
            } // End try catch
        } // End SendEmail

        /*
         * Name: CreateEmailMessage
         * Parameter: message(Message)
         * Author: Namchok Singhachai
         * Description: Create the email messages
         */
        private MimeMessage CreateEmailMessage(Message message)
        {
            try
            {
                _logger.LogTrace("Start Create Email Message.");
                var emailMessage = new MimeMessage();                           // New messages form
                _logger.LogTrace("Set email message.");
                emailMessage.From.Add(new MailboxAddress(_emailConfig.From));   // Set sender 
                emailMessage.To.AddRange(message.To);                           // Set reciver 
                emailMessage.Subject = message.Subject;                         // Set subject of email
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Content }; // Set content
                _logger.LogDebug("Creating message.");
                _logger.LogTrace("End Create Email Message.");
                return emailMessage;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                _logger.LogTrace("End Create Email Message.");
                return new MimeMessage();
            } // End try catch
        } // End CreateEmailMessage

        /*
         * Name: Send
         * Parameter: mailMessage(MimeMessage)
         * Author: Namchok Singhachai
         * Description: Send the email
         */
        private void Send(MimeMessage mailMessage)
        {
            try
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
                        _logger.LogInformation("Sending email.");
                        _logger.LogTrace("Email Sender: End Sending email.");
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e.Message.ToString());
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
            } catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                _logger.LogTrace("End Send Email.");
            } // Edn try catch
        }// End Send

        /*
         * Name: SendEmailAsync
         * Parameter: message(Message)
         * Author: Namchok Singhachai
         * Description: Create massage and send email
         */
        public async Task SendEmailAsync(Message message)
        {
            try
            {
                var mailMessage = CreateEmailMessage(message);
                _logger.LogTrace("Email Sender: Sending email (Async).");
                await SendAsync(mailMessage);
            } catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                _logger.LogTrace("End Send Email (async).");
            }
        } // End SendEmailAsync

        /*
         * Name: SendAsync
         * Parameter: mailMessage(MimeMessage)
         * Author: Namchok Singhachai
         * Description: Send the email
         */
        private async Task SendAsync(MimeMessage mailMessage)
        {
            try
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
                        _logger.LogInformation("Sending email.");
                        _logger.LogTrace("Email Sender: End Sending email (Async).");
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e.Message.ToString());
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
            } catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                _logger.LogTrace("End Send (async).");
            }
        } // End SendAsync
    } // End Class EmailSender
}
