using ASP_NET_Core_MVC_Project.Interfaces;
using ASP_NET_Core_MVC_Project.Domain.Events;
using Polly;
using MailKit.Net.Smtp;


namespace ASP_NET_Core_MVC_Project.Services
{
    public class ProductAddedEventHandler : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<ProductAddedEventHandler> _logger;
        private CancellationToken _cancelationToken;

        public ProductAddedEventHandler(
            IServiceScopeFactory serviceScopeFactory,
            ILogger<ProductAddedEventHandler> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;

            DomainEventsManager.Register<ProductAdded>(ev =>
            {
                _ = SendEmailNotification(ev);
            });
        }

        private async Task SendEmailNotification(ProductAdded ev)
        {
            await using var scope = _serviceScopeFactory.CreateAsyncScope();
            var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

            Task SendAsync(CancellationToken cancellationToken)
            {
                return emailSender.SendEmail(ev.Product, _cancelationToken);
            }

            var policy = Policy
                .Handle<SmtpCommandException>()
                .WaitAndRetryAsync(3,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(retryAttempt, 2)),
                    (exception, retryAttempt) =>
                    {
                        _logger.LogWarning(
                            exception, $"There was an error while sending e-mail. Retrying: {retryAttempt}");
                    });

            var result = await policy.ExecuteAndCaptureAsync(SendAsync, _cancelationToken);

            if (result.Outcome == OutcomeType.Failure)
            {
                _logger.LogError(result.FinalException, "There was an error while sending e-mail");
            }
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _cancelationToken = cancellationToken;
            return Task.CompletedTask;
        }
    }
}
