using System;

namespace Microservices.Ticketing.Web.Api.Shared.Model
{
    public class TicketModel
    {
        public string UserName { get; set; }
        public DateTime BookedOn { get; set; }
        public string Boarding { get; set; }
        public string Destination { get; set; }
    }
}
