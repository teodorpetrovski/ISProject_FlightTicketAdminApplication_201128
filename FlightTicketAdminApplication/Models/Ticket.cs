using System.ComponentModel.DataAnnotations;
using System;

namespace FlightTicketAdminApplication.Models
{
    public class Ticket
    {

       
        public string DepartureCity { get; set; }
        
        public string ArrivalCity { get; set; }

      
        public string DestinationImage { get; set; }

      
        public string FlightClass { get; set; }
        
        public string FlightDescription { get; set; }
        
        public double TicketPrice { get; set; }
        
        public int FlightDuration { get; set; }

     
        public DateTime DepartureDateTime { get; set; }
    }
}
