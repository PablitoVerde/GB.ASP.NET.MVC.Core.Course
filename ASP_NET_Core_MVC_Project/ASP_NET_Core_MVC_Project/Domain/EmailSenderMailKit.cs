using ASP_NET_Core_MVC_Project.Interfaces;
using MailKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace ASP_NET_Core_MVC_Project.Domain
{
    public class EmailSenderMailKit : IEmailSender
    {
        private readonly IConfigurationRoot _config;
        private readonly IOptions<SmtpCredentials> _smtpCredentials;
        private CancellationToken _cancelationToken;

        public EmailSenderMailKit(IConfigurationRoot config, IOptions<SmtpCredentials> options)
        {
            _config = config;
            _smtpCredentials = options;
        }
        public async Task SendEmail(Product product, CancellationToken cancellationToken)
        {
            string subject = "Уведомление: добавлен новый товар";
            string message = $"Артикул: {product.Id}. Название: {product.Name}. Описание {product.Description}. Ссылка на изображение {product.LinkToImage}";

            MimeMessage mimeMessageMail = PrepareMimeMessage(subject, message);

            await ConnectAndSend(mimeMessageMail);
        }

        public async Task SendInfo(string message, CancellationToken cancellationToken)
        {
            string subject = "Уведомление: сервер работает";
            string body = $"Уведомление: сервер работает. {message}";

            MimeMessage mimeMessageMail = PrepareMimeMessage(subject, body);

            await ConnectAndSend(mimeMessageMail);
        }

        private MimeMessage PrepareMimeMessage(string subject, string message)
        {
            MimeMessage mimeMessage = new();

            mimeMessage.From.Add(new MailboxAddress("Администрация", _config.GetSection("FromEmail").Value));
            mimeMessage.To.Add(new MailboxAddress("Администрация", _config.GetSection("ToEmail").Value));
            mimeMessage.Subject = subject;

            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            return mimeMessage;
        }

        private async Task ConnectAndSend(MimeMessage message)
        {
            var client = new SmtpClient();
            await client.ConnectAsync(_smtpCredentials.Value.SmtpServer, _smtpCredentials.Value.PortConnection, false);
            await client.AuthenticateAsync(_config.GetSection("FromEmail").Value, _config.GetSection("Pswd").Value);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
