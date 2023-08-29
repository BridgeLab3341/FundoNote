using MassTransit;
using Shared.Models;
using System.Threading.Tasks;

namespace TicketProcessor.Microservices.Consumers
{
    public class TicketConsumers : IConsumer<Ticket>
    {
        public async Task Consume(ConsumeContext<Ticket> context)
        {
            var data = context.Message;
            //Validate the Ticket Data
            //Store to Database
            //Notify the user via Email / SMS
        }
    }
}
