using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TaskApp.Models
{
    public enum TicketStatus
    {
        Open = 101,
        Assigned = 102,
        Review = 103,
        Closed = 104,
        Completed = 105
    }

    

    public class Ticket
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        //[Required(ErrorMessage ="Konu boş bırakılamaz." )]
        public string Subject { get; set; }
        public string Description { get; set; }
        public TicketStatus Status { get; set; }
        public short Rank { get; set; }
        public string Priortiy { get; set; }
        public DateTime OpenTime { get; set; }
        public DateTime AssignedTime { get; set; }
        public DateTime ReviewTime { get; set; }
        public DateTime ClosedTime { get; set; }
        public DateTime CompletedTime { get; set; }

        

            

       

    }
}
