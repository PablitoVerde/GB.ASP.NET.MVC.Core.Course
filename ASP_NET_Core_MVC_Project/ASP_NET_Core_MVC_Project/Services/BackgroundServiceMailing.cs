using System.Diagnostics;
using ASP_NET_Core_MVC_Project.Interfaces;
using ASP_NET_Core_MVC_Project.Domain;

namespace ASP_NET_Core_MVC_Project.Services
{
    public class BackgroundServiceMailing : BackgroundService
    {
        private readonly ILogger<BackgroundServiceMailing> _logger;
        private CancellationToken _cancelationToken;
        private readonly IEmailSender _emailSender;

        public BackgroundServiceMailing(ILogger<BackgroundServiceMailing> logger, IEmailSender emailSender)
        {
            _logger = logger;
            _emailSender = emailSender;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using var timer = new PeriodicTimer(TimeSpan.FromMinutes(60));
            Stopwatch sw = Stopwatch.StartNew();
            while (await timer.WaitForNextTickAsync(cancellationToken))
            {
                _emailSender.SendInfo(DateTime.Now.ToString() + "Онлайн " + sw.Elapsed, cancellationToken);
            }
        }
    }
}
