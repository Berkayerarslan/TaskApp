using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskApp.Models;
using TaskApp.Repositories;
using TaskApp.Services;

namespace TaskApp.Pages.ReviewTasks
{

    public class CloseTaskModel : PageModel
    {
        private readonly CustomerRepository _customerRepository;
        private readonly EmployeeRepository _employeeRepository;
        private readonly TicketRepository _ticketRepository;
        private readonly TicketService _ticketServise;

        public CloseTaskModel(CustomerRepository customerRepository, EmployeeRepository employeeRepository, TicketRepository ticketRepository, TicketService ticketService)
        {
            _customerRepository = customerRepository;
            _employeeRepository = employeeRepository;
            _ticketRepository = ticketRepository;
            _ticketServise = ticketService;

        }
        [BindProperty]
        public List<Ticket> Tickets { get; set; }
        [BindProperty]
        public List<Employee> Employees { get; set; } = new();
        public void OnGet()
        {
            Tickets = _ticketRepository.List().FindAll(x => x.Status == TicketStatus.Review);
            foreach (var item in Tickets)
            {
                Employees.Add(_employeeRepository.Find(item.EmployeeId));
            }

        }

        public void OnPostCloseTask(string id)
        {
            if (ModelState.IsValid)
            {
                var ticket = _ticketRepository.Find(id);
                _ticketServise.CloseTicket(ticket);

            }
            OnGet();
        }
    }
}
