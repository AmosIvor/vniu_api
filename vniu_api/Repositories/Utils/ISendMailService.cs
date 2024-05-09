using vniu_api.Models.EF.Utils;

namespace vniu_api.Repositories.Utils
{
    public interface ISendMailService
    {
        Task<bool> SendMail(MailData mailData);
        Task<bool> SendMailAsync(MailData mailData);
        Task<bool> SendHtmlMail(MailData mailData);
    }
}
