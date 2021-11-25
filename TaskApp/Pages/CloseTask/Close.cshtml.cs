using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskApp.Models;
using TaskApp.Repositories;
using TaskApp.Services;

namespace TaskApp.Pages.CloseTask
{
    public class CloseModel : PageModel
    {
        private readonly CustomerRepository _customerRepository;
        private readonly EmployeeRepository _employeeRepository;
        private readonly TicketRepository _ticketRepository;
        private readonly TicketService _ticketServise;

        public CloseModel(CustomerRepository customerRepository, EmployeeRepository employeeRepository, TicketRepository ticketRepository,TicketService ticketService)
        {
            _customerRepository = customerRepository;
            _employeeRepository = employeeRepository;
            _ticketRepository = ticketRepository;
            _ticketServise = ticketService;

        }
        public List<Ticket> Tickets { get; set; }
        public void OnGet()
        {

            Tickets = _ticketRepository.List().FindAll(x => x.Status == TicketStatus.Assigned);

        }
        public void OnPostClose()
        {

        }
    }
   
    
}
