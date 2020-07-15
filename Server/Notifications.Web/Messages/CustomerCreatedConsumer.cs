namespace Notifications.Web.Messages
{
    using Hub;
    using MassTransit;
    using Microsoft.AspNetCore.SignalR;
    using StoreApi.Web.Messages;
    using System.Threading.Tasks;

    using static WebConstants;

    public class CustomerCreatedConsumer : IConsumer<CustomerCreatedMessage>
    {
        private readonly IHubContext<NotificationsHub> hub;
        
        public CustomerCreatedConsumer(IHubContext<NotificationsHub> hub)
            => this.hub = hub;

        //ToDo: Restrict clients
        public async Task Consume(ConsumeContext<CustomerCreatedMessage> context)
        => await this.hub
                   .Clients
                   .All
                   .SendAsync(ReceiveNotificationEndpoint, context.Message);
    }
}