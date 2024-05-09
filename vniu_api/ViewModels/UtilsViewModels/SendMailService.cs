using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Org.BouncyCastle.Asn1.Pkcs;
using System.Net;
using vniu_api.Configuration;
using vniu_api.Models.EF.Utils;
using vniu_api.Repositories.Utils;

namespace vniu_api.ViewModels.UtilsViewModels
{
    public class SendMailService : ISendMailService
    {
        private readonly MailConfiguration _mailConfiguration;

        private readonly ILogger<SendMailService> _logger;

        public SendMailService(IOptions<MailConfiguration> mailConfigurationOptions, ILogger<SendMailService> logger)
        {
            _mailConfiguration = mailConfigurationOptions.Value;
            _logger = logger;
        }

        public Task<bool> SendHtmlMail(MailData mailData)
        {
            try
            {
                using (MimeMessage emailMessage = new MimeMessage())
                {
                    MailboxAddress emailFrom = new MailboxAddress(_mailConfiguration.SenderName, _mailConfiguration.SenderEmail);
                    emailMessage.From.Add(emailFrom);

                    //MailboxAddress emailTo = new MailboxAddress(htmlMailData.EmailToName, htmlMailData.EmailToId);
                    emailMessage.To.Add(MailboxAddress.Parse(mailData.MailTo));

                    emailMessage.Subject = "Hello";

                    string filePath = Directory.GetCurrentDirectory() + "\\Constants\\OrderTemplate.html";
                    string emailTemplateText = File.ReadAllText(filePath);

                    emailTemplateText = string.Format(emailTemplateText, mailData.MailTo, DateTime.Today.Date.ToShortDateString());

                    BodyBuilder emailBodyBuilder = new BodyBuilder();
                    emailBodyBuilder.HtmlBody = emailTemplateText;
                    emailBodyBuilder.TextBody = "Plain Text goes here to avoid marked as spam for some email servers.";

                    emailMessage.Body = emailBodyBuilder.ToMessageBody();

                    using (SmtpClient mailClient = new SmtpClient())
                    {
                        mailClient.Connect(_mailConfiguration.Server, _mailConfiguration.Port, MailKit.Security.SecureSocketOptions.StartTls);
                        mailClient.Authenticate(_mailConfiguration.SenderEmail, _mailConfiguration.Password);
                        mailClient.Send(emailMessage);
                        mailClient.Disconnect(true);
                    }
                }

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        public Task<bool> SendMail(MailData mailData)
        {
            try
            {
                using (MimeMessage emailMessage = new MimeMessage())
                {
                    MailboxAddress emailFrom = new MailboxAddress(_mailConfiguration.SenderName, _mailConfiguration.SenderEmail);
                    emailMessage.From.Add(emailFrom);

                    //MailboxAddress emailTo = new MailboxAddress(mailData.EmailToName, mailData.EmailToId);
                    emailMessage.To.Add(MailboxAddress.Parse(mailData.MailTo));

                    //emailMessage.Cc.Add(new MailboxAddress("Cc Receiver", "cc@example.com"));
                    //emailMessage.Bcc.Add(new MailboxAddress("Bcc Receiver", "bcc@example.com"));

                    emailMessage.Subject = mailData.MailSubject;

                    BodyBuilder emailBodyBuilder = new BodyBuilder();
                    emailBodyBuilder.TextBody = mailData.MailBody;

                    emailMessage.Body = emailBodyBuilder.ToMessageBody();
                    //this is the SmtpClient from the Mailkit.Net.Smtp namespace, not the System.Net.Mail one
                    using (SmtpClient mailClient = new SmtpClient())
                    {
                        mailClient.Connect(_mailConfiguration.Server, _mailConfiguration.Port, MailKit.Security.SecureSocketOptions.StartTls);
                        mailClient.Authenticate(_mailConfiguration.UserName, _mailConfiguration.Password);
                        mailClient.Send(emailMessage);
                        mailClient.Disconnect(true);
                    }
                }

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        public async Task<bool> SendMailAsync(MailData mailData)
        {
            try
            {
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                using (MimeMessage emailMessage = new MimeMessage())
                {
                    MailboxAddress emailFrom = new MailboxAddress(_mailConfiguration.SenderName, _mailConfiguration.SenderEmail);
                    emailMessage.From.Add(emailFrom);

                    //MailboxAddress emailTo = new MailboxAddress(mailData.EmailToName, mailData.EmailToId);
                    emailMessage.To.Add(MailboxAddress.Parse(mailData.MailTo));

                    // you can add the CCs and BCCs here.
                    //emailMessage.Cc.Add(new MailboxAddress("Cc Receiver", "cc@example.com"));
                    //emailMessage.Bcc.Add(new MailboxAddress("Bcc Receiver", "bcc@example.com"));

                    emailMessage.Subject = mailData.MailSubject;

                    BodyBuilder emailBodyBuilder = new BodyBuilder();
                    emailBodyBuilder.TextBody = mailData.MailBody;

                    emailMessage.Body = emailBodyBuilder.ToMessageBody();
                    //this is the SmtpClient from the Mailkit.Net.Smtp namespace, not the System.Net.Mail one
                    using (SmtpClient mailClient = new SmtpClient())
                    {
                        await mailClient.ConnectAsync(_mailConfiguration.Server, _mailConfiguration.Port, MailKit.Security.SecureSocketOptions.StartTls);
                        await mailClient.AuthenticateAsync(_mailConfiguration.SenderEmail, _mailConfiguration.Password);
                        await mailClient.SendAsync(emailMessage);
                        await mailClient.DisconnectAsync(true);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }
    }
}
