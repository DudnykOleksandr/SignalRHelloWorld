using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;

namespace Server
{
    public class TestServiсe : IHostedService, IDisposable
    {

        private const int DefaultSleepTime = 40;

        private Timer timer;

        private bool disposed = false;

        private readonly IHubContext<TestHub> testHubContext;

        public TestServiсe(IHubContext<TestHub> hubcontext)
        {
            this.testHubContext = hubcontext;
        }

        ~TestServiсe()
        {
            this.Dispose(false);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.timer = new Timer(
                this.DoWork,
                null,
                TimeSpan.Zero,
                TimeSpan.FromSeconds(1));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.timer?.Dispose();
            }

            this.disposed = true;
        }

        private void DoWork(object state)
        {
            this.testHubContext.Clients.All.SendAsync("testCall");
        }
    }
}
