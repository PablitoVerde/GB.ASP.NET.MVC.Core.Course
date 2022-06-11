using ASP_NET_Core_MVC_Project.Interfaces;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;

namespace ASP_NET_Core_MVC_Project.Models
{
    public class EmailSenderMailKit : IEmailSender
    {
        public async Task SendEmailAsync(Product product)
        {
            string ToEmail = "*****";

            string subject = "Уведомление: добавлен новый товар";
            string message = $"Артикул: {product.Id}. Название: {product.Name}. Описание {product.Description}. Ссылка на изображение {product.LinkToImage}";
            string FromEmail = "*****";
            int portConnection = 25;
            string smtpServer = "*****";
            string password = "*****";

            MimeMessage mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("Администрация", FromEmail));
            mimeMessage.To.Add(new MailboxAddress("Администрация", ToEmail));
            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtpServer, portConnection, false);
                await client.AuthenticateAsync(FromEmail, password);
                await client.SendAsync(mimeMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
