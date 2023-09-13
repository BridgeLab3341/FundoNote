using MassTransit;
using System.Threading.Tasks;
using System;

namespace CollaboratorConsumer
{
    public class CollaboratorConsumer : IConsumer<Collaborator>
    {
        public async Task Consume(ConsumeContext<Collaborator> context)
        {
            var collaborator = context.Message;

            // Perform any necessary processing (e.g., send acknowledgment email)
            Console.WriteLine($"Received collaborator data: UserId = {collaborator.UserId}, NoteId = {collaborator.NoteId}, Email = {collaborator.Email}");
        }
    }
}
