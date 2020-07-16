namespace Notifications.Web.Messages
{
    using MassTransit;
    using Statistics.Services.Contracts;
    using StoreApi.Web.Messages;
    using System.Threading.Tasks;

    public class SeenProductConsumer : IConsumer<SeenProductMessage>
    {
        private readonly ISeenProductService seenProductService;

        public SeenProductConsumer(ISeenProductService seenProductService)
            => this.seenProductService = seenProductService;

        public async Task Consume(ConsumeContext<SeenProductMessage> context)
            => await this.seenProductService.AddVisits(context.Message.Id);
    }
}