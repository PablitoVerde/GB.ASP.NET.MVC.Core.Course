using ASP_NET_Core_MVC_Project.Models;

namespace ASP_NET_Core_MVC_Project.Interfaces
{
    public interface IEmailSender
    {
        public Task SendEmailAsync(Product product);
    }
}
