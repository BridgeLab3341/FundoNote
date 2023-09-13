using CommonLayer.Model;
using MassTransit;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace TicketService.Service
{
    public class TicketConsumer : IConsumer<Collaborator>
    {
        public async Task Consume(ConsumeContext<Collaborator> context)
        {
            var data = context.Message;
            //Validate the Ticket Data
            //Store to Database
            //Notify the user via Email / SMS
            string email = data.Email;
            string subject = "Collabration Email";
            string body = "This Email Want to Collaborate with you";
            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("somubridgelabz@gmail.com", "crauayaudvfyhumw"),
                EnableSsl = true,
            };
            smtp.Send("somubridgelabz@gmail.com", email, subject, body);
        }
    }
}
