namespace StoreApi.Web.Messages
{
    using Hangfire;
    using MassTransit;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using StoreApi.Data.Models;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class MessagesHostedService : IHostedService
    {
        private readonly IServiceScopeFactory scopeFactory;
        private readonly IRecurringJobManager recurringJob;
        private readonly IBus publisher;

        public MessagesHostedService(
            IServiceScopeFactory scopeFactory,
            IRecurringJobManager recurringJob,
            IBus publisher)
        {
            this.scopeFactory = scopeFactory;
            this.recurringJob = recurringJob;
            this.publisher = publisher;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.recurringJob.AddOrUpdate(
                nameof(MessagesHostedService),
                () => this.ProcessPendingMessages(),
                "*/5 * * * * *");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;

        public void ProcessPendingMessages()
        {
            using (var scope = this.scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DbContext>();

                var messages = dbContext
                    .Set<Message>()
                    .Where(m => !m.Published)
                    .OrderBy(m => m.Id)
                    .ToList();

                foreach (var message in messages)
                {
                    this.publisher.Publish(message.Data, message.Type);

                    message.MarkAsPublished();

                    dbContext.SaveChanges();
                }
            }
        }
    }
}