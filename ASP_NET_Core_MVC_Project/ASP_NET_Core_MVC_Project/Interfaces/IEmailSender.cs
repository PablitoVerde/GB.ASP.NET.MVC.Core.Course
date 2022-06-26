using ASP_NET_Core_MVC_Project.Models;
using Microsoft.Extensions.Options;

namespace ASP_NET_Core_MVC_Project.Interfaces
{
    public interface IEmailSender
    {
        public void SendEmail(Product product, IConfigurationRoot configurationRoot, IOptions<SmtpCredentials> options);
    }
}
