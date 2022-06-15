using ASP_NET_Core_MVC_Project.Interfaces;
using MailKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace ASP_NET_Core_MVC_Project.Models
{
    public class EmailSenderMailKit : IEmailSender
    {
        public void SendEmail(Product product, IConfigurationRoot configurationRoot, IOptions<SmtpCredentials> options)
        {
            string subject = "Уведомление: добавлен новый товар";
            string message = $"Артикул: {product.Id}. Название: {product.Name}. Описание {product.Description}. Ссылка на изображение {product.LinkToImage}";

            MimeMessage mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("Администрация", configurationRoot.GetSection("FromEmail").Value));
            mimeMessage.To.Add(new MailboxAddress("Администрация", configurationRoot.GetSection("ToEmail").Value));
            mimeMessage.Subject = subject;
           
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            var client = new SmtpClient();
            client.Connect(options.Value.SmtpServer, options.Value.PortConnection, false);
            client.Authenticate(configurationRoot.GetSection("FromEmail").Value, configurationRoot.GetSection("Pswd").Value);
            client.Send(mimeMessage);
            client.Disconnect(true);
        }
    }
}
