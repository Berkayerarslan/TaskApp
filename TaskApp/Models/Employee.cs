using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskApp.Models
{
    public class Employee
    {
        public string Id { get; set; } 
        public string ManagerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public HashSet<Ticket> Tickets = new HashSet<Ticket>();

        
    }
}
