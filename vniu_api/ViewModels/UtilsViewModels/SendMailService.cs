using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Org.BouncyCastle.Asn1.Pkcs;
using System.Net;
using System.Text;
using vniu_api.Configuration;
using vniu_api.Models.EF.Products;
using vniu_api.Models.EF.Utils;
using vniu_api.Repositories.Utils;
using vniu_api.Templates.Ordered;

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

                    string filePath = Directory.GetCurrentDirectory() + "\\Templates\\Ordered\\OrderTemplate.html";
                    string emailTemplateText = File.ReadAllText(filePath);

                    // Dynamically generate date time
                    emailTemplateText = emailTemplateText.Replace("{DateTime}", DateTime.Now.ToString("MM/dd/yyyy HH:mm tt"));

                    // Dynamically generate product list
                    string productList = GenerateProductListHTML();
                    // Insert product list into the template
                    emailTemplateText = emailTemplateText.Replace("{ProductList}", productList);

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

        private string GenerateProductListHTML()
        {
            List<ProductTemplate> products = new List<ProductTemplate>
            {
                new ProductTemplate {ProductName = "T-Shirt", ProductQuantity = 2, ProductPrice = (float)18.7 },
                new ProductTemplate {ProductName = "Polo Shirt", ProductQuantity = 4, ProductPrice = (float)15.6 },
                new ProductTemplate {ProductName = "Hoodie", ProductQuantity = 5, ProductPrice = (float)2.1 },
                new ProductTemplate {ProductName = "Jeans", ProductQuantity = 1, ProductPrice = (float)3 },
                new ProductTemplate {ProductName = "Sneakers", ProductQuantity = 8, ProductPrice = (float)99.5 }
            };

            StringBuilder productListHTML = new StringBuilder();
            foreach (var product in products)
            {
                productListHTML.AppendLine($"<tr style=\"margin: 0;padding: 0;box-sizing: border-box;\">");
                productListHTML.AppendLine($"    <td class=\"product-name\" style=\"margin: 0;padding: 10px;box-sizing: border-box;text-align: center;border: 1px solid #ddd;width: 70;\">{product.ProductQuantity} x {product.ProductName}</td>");
                productListHTML.AppendLine($"    <td class=\"product-price\" style=\"margin: 0;padding: 10px;box-sizing: border-box;text-align: center;border: 1px solid #ddd;width: 30;\">{product.ProductPrice} $</td>");
                productListHTML.AppendLine("</tr>");
            }

            return productListHTML.ToString();
        }
    }
}
